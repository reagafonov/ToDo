using AutoMapper;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.FiltersData;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Services;

public class UserTaskListService(IRepository<UserTaskList, UserTaskListFilterData> userTaskListRepositrty,
    IMapper mapper)
    : IUserTaskListService
{
    /// <summary>
    /// Получение списков задач 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<UserTaskListDto>> GetUserTasksListsAsync(CancellationToken cancellationToken)
    {
        UserTaskListFilterData filter = new UserTaskListFilterData();
        IEnumerable<UserTaskList> userTaskLists = await userTaskListRepositrty.GetFilteredAsync(filter, cancellationToken);
        return mapper.Map<List<UserTaskListDto>>(userTaskLists);
    }

    /// <summary>
    /// Добавление списка
    /// </summary>
    /// <param name="userTaskListDto">Данные списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданного списка</returns>
    public async Task<Guid> AddUserTaskListAsync(UserTaskListDto userTaskListDto, CancellationToken cancellationToken)
    {
        UserTaskList userTaskList = mapper.Map<UserTaskList>(userTaskListDto);
        
        return await userTaskListRepositrty.AddAsync(userTaskList, cancellationToken);
    }

    /// <summary>
    /// Обновление данных списка
    /// </summary>
    /// <param name="userTaskListDto">Новые данные списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task UpdateUserTaskListAsync(UserTaskListDto userTaskListDto, CancellationToken cancellationToken)
    {
        UserTaskList userTaskList = mapper.Map<UserTaskList>(userTaskListDto);
        
        UserTaskList currentData = await userTaskListRepositrty.GetAsync(userTaskList.Id, cancellationToken);

        if (currentData.OwnerUserId != userTaskListDto.OwnerUserId)
            throw new UnauthorizedAccessException();
        
        await userTaskListRepositrty.UpdateAsync(userTaskList, cancellationToken);
    }

    /// <summary>
    /// Удаление данных списка 
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task DeleteUserTaskListAsync(Guid id, CancellationToken cancellationToken)
    {
        await userTaskListRepositrty.DeleteAsync(id, cancellationToken);
    }
}