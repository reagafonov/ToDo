using AutoMapper;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.FiltersData;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Services;

/// <summary>
/// Сервис задач
/// </summary>
/// <param name="repository">Репозиторий задач</param>
/// <param name="mapper">Автомаппер</param>
public class UserTaskService(IRepository<UserTask, UserTaskFilterData> repository, IMapper mapper, IUserTaskListOrderService orderService):IUserTaskService
{

    /// <summary>
    /// Получение данных задачи
    /// </summary>
    /// <param name="listId">Идентификатор списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Данные задачи</returns>
    public async Task<IEnumerable<UserTaskDto>> GetUserTasks(Guid listId,
        CancellationToken cancellationToken)
    {
        UserTaskFilterDto filter = new()
        {
            UserTaskListId = listId,
            WithDeleted = false,
        };
        
        UserTaskFilterData? filterData = mapper.Map<UserTaskFilterData>(filter);
        
        IEnumerable<UserTask?> tasks =  await repository.GetFilteredAsync(filterData, cancellationToken);
        
        List<UserTaskDto> dtos = mapper.Map<List<UserTaskDto>>(tasks);
        
        List<UserTaskDto> userTaskDtos = await orderService.OrderAsync(listId, dtos, cancellationToken);
        
        return userTaskDtos;
    }
    
    /// <summary>
    /// Получение данных по таске
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача</returns>
    public async Task<UserTaskDto> GetUserTaskAsync(Guid id, CancellationToken cancellationToken)
    {
        UserTask? userTask = await repository.GetAsync(id, cancellationToken);
        if (userTask == null)
            throw new KeyNotFoundException();
        return mapper.Map<UserTaskDto>(userTask);
    }

    /// <summary>
    /// Фильтрация по различным полям
    /// </summary>
    /// <param name="filter">Класс фильтра</param>
    /// <param name="ordered">Если нужна сортировка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Отфильтрованные данные задачи</returns>
    public async Task<List<UserTaskDto>> GetUserTasksAsync(UserTaskFilterDto filter, bool ordered,
        CancellationToken cancellationToken)
    {
        UserTaskFilterData? filterData = mapper.Map<UserTaskFilterData>(filter);
        IEnumerable<UserTask?> tasks =  await repository.GetFilteredAsync(filterData, cancellationToken);
        List<UserTaskDto> dtos = mapper.Map<List<UserTaskDto>>(tasks);
        if (filterData.UserTaskListId.HasValue)
            dtos = await orderService.OrderAsync(filter.UserTaskListId, dtos, cancellationToken);
        return dtos;
    }
    
    /// <summary>
    /// Создание задачи
    /// </summary>
    /// <param name="userTaskDto">Данные задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<Guid> AddAsync(UserTaskDto userTaskDto, CancellationToken cancellationToken)
    {
        userTaskDto.Created = DateTime.Now;
        UserTask? userTask = mapper.Map<UserTask>(userTaskDto);
        await repository.AddAsync(userTask, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return userTask.Id;
    }

    /// <summary>
    /// Редактирование задачи
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="dto">Отредактированные данные задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task EditAsync(Guid id, UserTaskUpdateDto dto, CancellationToken cancellationToken)
    {
        UserTask? userTask = await repository.GetAsync(id, cancellationToken);
        
        if (userTask == null)
            throw new KeyNotFoundException();
        
        mapper.Map(dto, userTask);
        
        SetCompletedDate(userTask); 
        
        if (userTask is { IsCompleted: true, CompleteDate: null })
            userTask.CompleteDate = DateTime.UtcNow;
        
        await repository.UpdateAsync(userTask, cancellationToken);
        
        await repository.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Пометка задачи как выполненной
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="isCompleted">Флаг выполненности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task MarkAsCompletedAsync(Guid id, bool isCompleted, CancellationToken cancellationToken)
    {
        UserTask? userTask = await repository.GetAsync(id, cancellationToken);
       
        if (userTask == null)
            throw new KeyNotFoundException();
        
        UserTaskDto userTaskDto = mapper.Map<UserTaskDto>(userTask);
        if (isCompleted == userTaskDto.IsCompleted)
            return;
        
        userTaskDto.IsCompleted = isCompleted;
        
        mapper.Map(userTaskDto, userTask);
        
        SetCompletedDate(userTask); 
        
        await repository.UpdateAsync(userTask, cancellationToken);
        
        await repository.SaveChangesAsync(cancellationToken);
    }

    private void SetCompletedDate(UserTask userTask)
    {
        var dto = mapper.Map<UserTaskDto>(userTask);
        dto.CompleteDate = dto.IsCompleted ? DateTime.Now : null;
        mapper.Map(dto, userTask);
    }

    /// <summary>
    /// Удаление задачи
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Удаление множества задач
    /// </summary>
    /// <param name="ids">Идентификаторы задач</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Удаленные идентификаторы</returns>
    public async Task<IEnumerable<Guid>> DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        List<UserTask>? userTasks = await repository.GetFilteredAsync(new UserTaskFilterData()
                                                {
                                                    Ids = ids
                                                }, cancellationToken);
        if (userTasks == null)
            throw new KeyNotFoundException();
        
        await repository.DeleteAsync(userTasks, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return userTasks.Select(t => t.Id);
    }

    /// <summary>
    /// Восстановление задач
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task UndeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await repository.RestoreAsync(id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
    
}