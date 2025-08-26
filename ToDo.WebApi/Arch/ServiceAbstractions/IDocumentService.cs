using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.ServiceAbstractions;

/// <summary>
/// Сервис для работы файлами
/// </summary>
public interface IDocumentService
{
    /// <summary>
    /// Сохранение файла
    /// </summary>
    /// <param name="document">Данные файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<string> AddAsync(UserTaskFileDto document, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получение метаданных файла для задачи
    /// </summary>
    /// <param name="userTaskId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<UserTaskFileSimpleDto>> GetFromTaskAsync(Guid userTaskId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение данных файла для задачи
    /// </summary>
    /// <param name="id">Идентификатор файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<Stream> GetContentsByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение метаданных файла для задачи
    /// </summary>
    /// <param name="id">Идентификатор файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Данные о файле</returns>
    Task<UserTaskFileSimpleDto> GetInfoByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаление файла по ID
    /// </summary>
    /// <param name="id">Идентифкатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
   
}