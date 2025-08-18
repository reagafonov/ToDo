using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.ServiceAbstractions;

/// <summary>
/// Сервис списков задач
/// </summary>
public interface IUserTaskListService
{
    /// <summary>
    /// Получение списков задач 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<UserTaskListDto>> GetUserTasksListsAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавление списка
    /// </summary>
    /// <param name="userTaskListDto">Данные списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданного списка</returns>
    Task<Guid> AddUserTaskListAsync( UserTaskListDto userTaskListDto, CancellationToken cancellationToken );
    
    /// <summary>
    /// Обновление данных списка
    /// </summary>
    /// <param name="userTaskListDto">Новые данные списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task UpdateUserTaskListAsync( UserTaskListDto userTaskListDto, CancellationToken cancellationToken );

    /// <summary>
    /// Удаление данных списка 
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteUserTaskListAsync( Guid id, CancellationToken cancellationToken );
    
}