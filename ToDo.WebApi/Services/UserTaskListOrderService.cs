using AutoMapper;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.FiltersData;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Services;

/// <summary>
/// Сервис сортировки списков задач
/// </summary>
/// <param name="taskListRepository">Репозиторий списков задач</param>
/// <param name="mapper">Автомаппер</param>
public class UserTaskListOrderService(IRepository<UserTaskList, UserTaskListFilterData> taskListRepository, IMapper mapper) : IUserTaskListOrderService
{
    /// <summary>
    /// Сортировки списка задач, по указанному порядку
    /// </summary>
    /// <param name="listId">Идентификатор списка</param>
    /// <param name="tasks">Сортируемые задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Отсортированный список</returns>
    /// <exception cref="ArgumentOutOfRangeException">Если заданная в параметрах сортировка не поддерживается</exception>
    /// <exception cref="KeyNotFoundException">Если не найдены данные списка</exception>
    public async Task<List<UserTaskDto>> OrderAsync(Guid listId, IEnumerable<UserTaskDto> tasks,
        CancellationToken cancellationToken)
    {
        UserTaskList? taskList = await taskListRepository.GetAsync(listId, cancellationToken);
        
        if (taskList == null)
            throw new KeyNotFoundException();

        UserTaskListDto taskListDto = mapper.Map<UserTaskListDto>(tasks);
        
        List<UserTaskDto> sortedTasks;
        switch (taskListDto.OrderType)
        {
            case UserListOrderTypeEnum.Priority:
                sortedTasks = tasks.OrderBy(dto => dto.Priority).ToList();
                break;
            case UserListOrderTypeEnum.CreationDate:
                sortedTasks = tasks.OrderBy(dto => dto.Created).ToList();
                break;
            case UserListOrderTypeEnum.CompletedDate:
                sortedTasks = tasks.OrderBy(dto => dto.CompleteDate).ToList();
                break;
            case UserListOrderTypeEnum.Alphabetical:
                sortedTasks = tasks.OrderBy(dto => dto.Name).ToList();
                break;
            case UserListOrderTypeEnum.Undefined:
                sortedTasks = tasks.ToList();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (taskListDto.OrderDirection)
        {
            case OrderDirectionEnum.Ascending:
                break;
            case OrderDirectionEnum.Descending:
                sortedTasks.Reverse();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        int order = 0;
        foreach (UserTaskDto task in sortedTasks)
        {
            task.Order = order;
            order++;
        }

        return sortedTasks;
    }
}