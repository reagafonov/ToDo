using System.Data;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using ToDo.WebApi.Domain;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.MongoService;

/// <summary>
/// Реализация сервиса для работы с маленькими файлами
/// </summary>
public class MongoSmallDocumentService(MongoContext context, IMapper mapper) : IDocumentService
{
    /// <summary>
    /// Название поля для даты создания
    /// </summary>
    private const string UploadDateFieldName = "uploadDate";

    private const string NameFieldName = "fileName";
    private const string UserTaskIdFieldName = "userTaskId";
    private const string IdFieldName = "_id";
    private const string DataFieldName = "data";

    private readonly IMongoCollection<BsonDocument> _collection = context.DocumentCollection.Value;

    /// <summary>
    /// Сохранение файла
    /// </summary>
    /// <param name="document">Данные файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<string> AddAsync(UserTaskFileDto document, CancellationToken cancellationToken = default)
    {
        using MemoryStream stream = new MemoryStream();

        await document.Contents.CopyToAsync(stream, cancellationToken);

        BsonDocument bsonDocument = new BsonDocument
        {
            { NameFieldName, document.Name },
            { UserTaskIdFieldName, document.UserTaskId.ToString() },
            { UploadDateFieldName, document.Created },
            { DataFieldName, stream.GetBuffer().Take((int)stream.Length).ToArray() }
        };
        await _collection.InsertOneAsync(bsonDocument, new InsertOneOptions(), cancellationToken);

        return bsonDocument.GetElement(IdFieldName).Value.ToString() ?? throw new DataException("Id not found");
    }

    /// <summary>
    /// Получение данных файла для задачи
    /// </summary>
    /// <param name="userTaskId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<UserTaskFileSimpleDto>> GetFromTaskAsync(Guid userTaskId,
        CancellationToken cancellationToken = default)
    {
        string taskId = userTaskId.ToString();
        List<BsonDocument>? files = await _collection.Find(x => x[UserTaskIdFieldName].AsString == taskId)
            .ToListAsync(cancellationToken);

        List<UserTaskFileSimpleDto> dtos = files.Select(mapper.Map<UserTaskFileSimpleDto>).ToList();

        return dtos;
    }

    /// <summary>
    /// Получение данных файла для задачи
    /// </summary>
    /// <param name="id">Идентификатор файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<Stream> GetContentsByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ObjectId objectId = new ObjectId(id);
        BsonDocument? files =
            await _collection.Find(x => x[IdFieldName] == objectId).FirstOrDefaultAsync(cancellationToken);

        if (files == null)
            throw new KeyNotFoundException($"File with id {id} not found");

        MemoryStream stream = new MemoryStream(files[DataFieldName].AsByteArray);

        return stream;
    }

    /// <summary>
    /// Получает данные о файле по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task<UserTaskFileSimpleDto> GetInfoByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ObjectId objectId = new ObjectId(id);
        BsonDocument? file =
            await _collection.Find(x => x[IdFieldName] == objectId).FirstOrDefaultAsync(cancellationToken);

        if (file == null)
            throw new KeyNotFoundException($"File with id {id} not found");

        UserTaskFileSimpleDto dto = mapper.Map<UserTaskFileSimpleDto>(file);

        return dto;
    }

    /// <summary>
    /// Удаление файла по ID
    /// </summary>
    /// <param name="id">Идентифкатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        DeleteResult? result = await _collection.DeleteOneAsync(x => x[IdFieldName] == new ObjectId(id), cancellationToken);
        
        if (result.DeletedCount == 0)
            throw new KeyNotFoundException();
    }
}