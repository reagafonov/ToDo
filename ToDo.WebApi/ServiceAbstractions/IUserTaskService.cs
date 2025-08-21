using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.ServiceAbstractions;

/// <summary>
///  Интерфейс сервиса задачи
/// </summary>
public interface IUserTaskService
{
    /// <summary>
    /// Фильтрация по различным полям
    /// </summary>
    /// <param name="filter">Класс фильтра</param>
    /// <param name="ordered">Если нужна сортировка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Отфильтрованные данные задачи</returns>
    Task<List<UserTaskDto>> GetUserTasksAsync(UserTaskFilterDto filter, bool ordered,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получение данных по таске
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача</returns>
    Task<UserTaskDto> GetUserTaskAsync(Guid id, CancellationToken cancellationToken);

    
    /// <summary>
    /// Создание задачи
    /// </summary>
    /// <param name="userTaskDto">Данные задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<Guid> AddAsync(UserTaskDto userTaskDto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Редактирование задачи
    /// </summary>
    /// <param name="userTaskDto">Отредактированные данные задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task EditAsync(UserTaskDto userTaskDto, CancellationToken cancellationToken);

    /// <summary>
    /// Пометка задачи как выполненой
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="isCompleted">Флаг выполненности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task MarkAsCompletedAsync(Guid id, bool isCompleted, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление задачи
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task RemoveAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаление множества задач
    /// </summary>
    /// <param name="ids">Идентификаторы задач</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Удаленные идентификаторы</returns>
    Task<IEnumerable<Guid>> DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    
    /// <summary>
    /// Восстановление задач
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task UndeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение данных задачи
    /// </summary>
    /// <param name="listId">Идентификатор списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Данные задачи</returns>
    Task<IEnumerable<UserTaskDto>> GetUserTasks(Guid listId,
        CancellationToken cancellationToken);
}