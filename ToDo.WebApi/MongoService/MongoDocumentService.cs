using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using ToDo.WebApi.MongoDomain;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.MongoService;

/// <summary>
/// Реализация сервиса файлов для монго
/// </summary>
public class MongoDocumentService(IMongoDatabase database):IDocumentService
{
    /// <summary>
    /// Название поля для даты создания
    /// </summary>
    private const string UploadDateFieldName = "uploadDate";
    private const string NameFieldName = "fileName";
    private const string UserTaskIdFieldName = "userTaskId";
    private const string IdFieldName = "_id";

    private Func<GridFSFileInfo, UserTaskFileSimpleDto> toDto = x => new UserTaskFileSimpleDto()
    {
         Created = x.Metadata[UploadDateFieldName].AsUniversalTime,
         Name = x.Metadata[NameFieldName]?.ToString() ?? string.Empty,
         UserTaskId = Guid.Parse(x.Metadata[UserTaskIdFieldName].AsString),
         Id = x.Id.ToString()
    };
    
    private Func< UserTaskFileDto, BsonDocument> toBson = document => new BsonDocument()
    {
        {UploadDateFieldName, document.Created},
        {NameFieldName, document.Name},
        {UserTaskIdFieldName, document.UserTaskId.ToString()},
        {IdFieldName, document.Id.ToString()}
    };

    private readonly GridFSBucket _gridFsBucket = new GridFSBucket(database);


    /// <summary>
    /// Сохранение файла
    /// </summary>
    /// <param name="document">Данные файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<string> AddAsync(UserTaskFileDto document, CancellationToken cancellationToken = default)
    {
        GridFSBucket gridFsBucket = new GridFSBucket(database);

        ObjectId uploadFromStreamAsync = await gridFsBucket.UploadFromStreamAsync(document.Name, document.Contents, new GridFSUploadOptions()
        {
            Metadata = toBson(document)
        }, cancellationToken);
        
        return uploadFromStreamAsync.ToString();
    }

    /// <summary>
    /// Получение данных файла для задачи
    /// </summary>
    /// <param name="userTaskId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<UserTaskFileSimpleDto>> GetFromTaskAsync(Guid userTaskId, CancellationToken cancellationToken = default)
    {
        string userTaskIdString = userTaskId.ToString();
        FilterDefinition<GridFSFileInfo> eqFilter = Builders<GridFSFileInfo>.Filter.Eq(x=>x.Metadata[UserTaskIdFieldName], userTaskIdString);

        List<GridFSFileInfo> filesCollection = await database.GetCollection<GridFSFileInfo>("fs.files").Find(eqFilter)
                .ToListAsync(cancellationToken);

        List<UserTaskFileSimpleDto> files = filesCollection.Select(toDto).ToList();

        return files;
    }

    /// <summary>
    /// Получение данных файла для задачи
    /// </summary>
    /// <param name="id">Идентификатор файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<Stream> GetContentsByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ObjectId fileId = new ObjectId(id);
        return await _gridFsBucket.OpenDownloadStreamAsync(fileId, null,cancellationToken);
    }

    /// <summary>
    /// Получает данные о файле по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task<UserTaskFileSimpleDto> GetInfoByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var objectId = new ObjectId(id);
        GridFSFileInfo?  filesCollection =
            await database.GetCollection<GridFSFileInfo>("fs.files").Find(x=> x.Id == objectId).FirstOrDefaultAsync(cancellationToken);

        if (filesCollection == null)
            throw new KeyNotFoundException();
        
        UserTaskFileSimpleDto fileInfo= toDto(filesCollection);
        fileInfo.Id = id;
        return fileInfo;
    }


    /// <summary>
    /// Удаление файла по ID
    /// </summary>
    /// <param name="id">Идентифкатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        GridFSBucket gridFsBucket = new GridFSBucket(database);
        await gridFsBucket.DeleteAsync(new ObjectId(id), cancellationToken);
    }
}