using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.ServiceAbstractions;

/// <summary>
/// Сервис сортировки
/// </summary>
public interface IUserTaskListOrderService
{
    /// <summary>
    /// Сортировки списка задач, по указанному порядку
    /// </summary>
    /// <param name="listId">Идентификатор списка</param>
    /// <param name="tasks">Сортируемые задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Отсортированный список</returns>
    /// <exception cref="ArgumentOutOfRangeException">Если заданная в параметрах сортировка не поддерживается</exception>
    Task<List<UserTaskDto>> OrderAsync(Guid listId, IEnumerable<UserTaskDto> tasks,
        CancellationToken cancellationToken);
}