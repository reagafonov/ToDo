using AutoMapper;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.FiltersData;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Services;

public class UserTaskService(IRepository<UserTask, UserTaskFilterData> repository, IRepository<UserTaskList, UserTaskListFilterData> listRepository, IMapper mapper):IUserTaskService
{

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
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Отфильтрованные данные задачи</returns>
    public async Task<List<UserTaskDto>> GetUserTasksAsync(UserTaskFilterDto filter, CancellationToken cancellationToken)
    {
        UserTaskFilterData? filterData = mapper.Map<UserTaskFilterData>(filter);
        IEnumerable<UserTask?> tasks =  await repository.GetFilteredAsync(filterData, cancellationToken);
        return mapper.Map<List<UserTaskDto>>(tasks);
    }
    public async Task<Guid> AddAsync(UserTaskDto userTaskDto, CancellationToken cancellationToken)
    {
        UserTask? userTask = mapper.Map<UserTask>(userTaskDto);
        return await repository.AddAsync(userTask, cancellationToken); 
    }

    public async Task EditAsync(UserTaskDto userTaskDto, CancellationToken cancellationToken)
    {
        UserTask? userTask = mapper.Map<UserTask>(userTaskDto);
        await repository.UpdateAsync(userTask, cancellationToken);
    }

    public async Task MarkAsCompletedAsync(Guid id, bool isCompleted, CancellationToken cancellationToken)
    {
        UserTask? userTask = await repository.GetAsync(id, cancellationToken);
        if (userTask == null)
            throw new KeyNotFoundException();
        userTask.IsCompleted = isCompleted;
        await repository.UpdateAsync(userTask, cancellationToken);
    }

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Guid>> DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return await repository.DeleteAsync(ids, cancellationToken);
    }

    public async Task UndeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await repository.RestoreAsync(id, cancellationToken);
    }
}