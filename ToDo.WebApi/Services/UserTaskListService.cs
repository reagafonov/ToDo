using AutoMapper;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.FiltersData;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Services;

/// <summary>
/// Сервис для работы со списками задач
/// </summary>
/// <param name="userTaskListRepository">Репозиторий списков задач</param>
/// <param name="mapper">Автомаппер</param>
public class UserTaskListService(IRepository<UserTaskList, UserTaskListFilterData> userTaskListRepository,
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
        IEnumerable<UserTaskList> userTaskLists = await userTaskListRepository.GetFilteredAsync(filter, cancellationToken);
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
        
        await userTaskListRepository.AddAsync(userTaskList, cancellationToken);
        
        await userTaskListRepository.SaveChangesAsync(cancellationToken);
        
        return userTaskList.Id;
    }

    /// <summary>
    /// Обновление данных списка
    /// </summary>
    /// <param name="userTaskListDto">Новые данные списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task UpdateUserTaskListAsync(UserTaskListDto userTaskListDto, CancellationToken cancellationToken)
    {
        
        UserTaskList? currentData = await userTaskListRepository.GetAsync(userTaskListDto.Id, cancellationToken);

        if (currentData == null)
            throw new KeyNotFoundException();
        
        if (currentData.OwnerUserId != userTaskListDto.OwnerUserId)
            throw new UnauthorizedAccessException();
        
        mapper.Map(userTaskListDto, currentData);
        
        await userTaskListRepository.UpdateAsync(currentData, cancellationToken);
        
        await userTaskListRepository.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Удаление данных списка 
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task DeleteUserTaskListAsync(Guid id, CancellationToken cancellationToken)
    {
        await userTaskListRepository.DeleteAsync(id, cancellationToken);
        await userTaskListRepository.SaveChangesAsync(cancellationToken);
    }
}