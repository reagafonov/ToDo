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
public class UserTaskService(IRepository<UserTask, UserTaskFilterData> repository, IMapper mapper):IUserTaskService
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
    
    /// <summary>
    /// Создание задачи
    /// </summary>
    /// <param name="userTaskDto">Данные задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<Guid> AddAsync(UserTaskDto userTaskDto, CancellationToken cancellationToken)
    {
        UserTask? userTask = mapper.Map<UserTask>(userTaskDto);
        await repository.AddAsync(userTask, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return userTask.Id;
    }

    /// <summary>
    /// Редактирование задачи
    /// </summary>
    /// <param name="userTaskDto">Отредактированные данные задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task EditAsync(UserTaskDto userTaskDto, CancellationToken cancellationToken)
    {
        UserTask? userTask = await repository.GetAsync(userTaskDto.Id, cancellationToken);
        if (userTask == null)
            throw new KeyNotFoundException();
        mapper.Map(userTaskDto, userTask);
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
        userTask.IsCompleted = isCompleted;
        await repository.UpdateAsync(userTask, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
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