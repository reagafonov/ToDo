using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDo.WebApi.Models;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ToDo.WebApi.Controllers;

/// <summary>
/// Контроллер списков
/// </summary>
/// <param name="service">Сервис списков</param>
/// <param name="mapper">automapper</param>
[ApiController]
[Route("v1/[controller]")]
[Authorize]
public class TaskListController(IUserTaskListService service, IMapper mapper, ILogger<TaskListController> logger, IUserServiceClaims userServiceClaims):ControllerBase
{
    /// <summary>
    /// Получение всех неудаленных списков для пользователя
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Данные списков</returns>
    [HttpGet]
    public async Task<List<UserTaskListModel>> GetAllUndeletedTaskListsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Запрос на получение всех неудаленных списков");
        
        List<UserTaskListDto> lists = await service.GetUserTasksListsAsync(cancellationToken);
        
        return mapper.Map<List<UserTaskListModel>>(lists);
    }
    
    /// <summary>
    /// Получение всех списков
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Данные списков</returns>
    [HttpGet("all")]
    public async Task<List<UserTaskListModel>> GetAllTaskListsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Получение всех списков");
        
        List<UserTaskListDto> lists = await service.GetUserTasksListsAsync(cancellationToken);
        
        return mapper.Map<List<UserTaskListModel>>(lists);
    }

    /// <summary>
    /// Добавление писка задач
    /// </summary>
    /// <param name="taskListModel">Данные списка задач</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданной задачи</returns>
    [HttpPost]
    public async Task<Guid> AddTaskListAsync(UserTaskListAddModel taskListModel,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Добавление списка задач");
        logger.LogDebug(taskListModel.ToString());
        
        UserTaskListDto? dto = mapper.Map<UserTaskListDto>(taskListModel);
        dto.OwnerUserId = await userServiceClaims.GetCurrentUserIdAsync(User);
        
        return await service.AddUserTaskListAsync(dto, cancellationToken);
    }

    /// <summary>
    /// Редактирование данных списка
    /// </summary>
    /// <param name="id">Идентификатор редактируемого списка</param>
    /// <param name="taskListModel">Отредактированные данные</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPut("{id:guid}")]
    public async Task UpdateUserTaskList(Guid id, UserTaskListAddModel taskListModel, CancellationToken cancellationToken)
    {
        logger.LogInformation("Редактирование данных списка");
        logger.LogDebug("Идентификатор");
        logger.LogDebug(id.ToString());
        logger.LogDebug("Данные");
        logger.LogDebug(taskListModel.ToString());
        
        UserTaskListDto? dto = mapper.Map<UserTaskListDto>(taskListModel);
        dto.Id = id;
        dto.OwnerUserId = await userServiceClaims.GetCurrentUserIdAsync(User);
        
        await service.UpdateUserTaskListAsync(dto, cancellationToken);
    }

    /// <summary>
    /// Удаление списка
    /// </summary>
    /// <param name="id">Идентификатор удаляемого списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("{id:guid}")]
    public async Task DeleteUserTaskList(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Удаление списка");
        logger.LogDebug("Идентификатор");
        logger.LogDebug(id.ToString());
        
        await service.DeleteUserTaskListAsync(id, cancellationToken);
    }
}