using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.WebApi.Models;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Controllers;

/// <summary>
/// Контроллер Web api для задач
/// </summary>
/// <param name="userTaskService">Сервис задач</param>
/// <param name="mapper">automapper</param>
[ApiController]
[Route("v1/[controller]")]
[Authorize]
public class TaskController(IUserTaskService userTaskService, IMapper mapper, IUserServiceClaims userServiceClaims):ControllerBase
{

    /// <summary>
    /// Получает все задачи, включая удаленные, для списка
    /// </summary>
    /// <param name="listId">Идентификатор списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задачи</returns>
    [HttpGet("list/{listId:guid}/all")]
    public async Task<List<UserTaskSmallModel>> GetAllUserTasks([FromRoute]Guid listId, CancellationToken cancellationToken)
    {
        UserTaskFilterDto userTaskFilterDto = new UserTaskFilterDto()
        {
            WithDeleted = true,
            UserTaskListId = listId
        };
        
        List<UserTaskDto> allTasks = await userTaskService.GetUserTasksAsync(userTaskFilterDto, cancellationToken);
        return mapper.Map<List<UserTaskSmallModel>>(allTasks);
    }
    
    /// <summary>
    /// Получение данных задачи
    /// </summary>
    /// <param name="listId">Идентификатор списка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Данные задачи</returns>
    [HttpGet("list/{listId:guid}")]
    public async Task<IEnumerable<UserTaskSmallModel?>> GetUserTasks([FromRoute]Guid listId, CancellationToken cancellationToken)
    {
        UserTaskFilterDto filter = new UserTaskFilterDto()
        {
            UserTaskListId = listId
        };
        List<UserTaskDto> userTasksAsync = await userTaskService.GetUserTasksAsync(filter, cancellationToken);
        return mapper.Map<List<UserTaskSmallModel>>(userTasksAsync);
    }

    /// <summary>
    /// Получение подробной модели для задачи по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Подробная модель задачи</returns>
    [HttpGet("{id:guid}")]
    public async Task<UserTaskModel?> GetUserTask([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        UserTaskDto data = await userTaskService.GetUserTaskAsync(id, cancellationToken);
        return mapper.Map<UserTaskModel>(data);
    }

    /// <summary>
    /// Добавление задачи
    /// </summary>
    /// <param name="userTaskModel">Данные задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор новой задачи</returns>
    [HttpPost]
    public async Task<Guid> AddAsync([FromBody] UserTaskAddModel userTaskModel, CancellationToken cancellationToken)
    {
        UserTaskDto? dto = mapper.Map<UserTaskDto>(userTaskModel);
        dto.OwnerUserId = await userServiceClaims.GetCurrentUserIdAsync(User);
        Guid userTaskId = await userTaskService.AddAsync(dto, cancellationToken);
        return userTaskId;
    }

    /// <summary>
    /// Пометка задачи как выполненной
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPatch("{id:guid}/complete")]
    public async Task Complete([FromRoute]Guid id, CancellationToken cancellationToken)
    {
        await userTaskService.MarkAsCompletedAsync(id,true, cancellationToken);
    }

    /// <summary>
    /// Пометка задачи как невыполненной
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPatch("{id:guid}/uncomplete")]
    public async Task SetUncompletedAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await userTaskService.MarkAsCompletedAsync(id,false, cancellationToken);
    }

    /// <summary>
    /// Удаление задачи
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("{id:guid}")]
    public async Task DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await userTaskService.RemoveAsync(id, cancellationToken);
    }

    /// <summary>
    /// Удаление списка задач
    /// </summary>
    /// <param name="ids">Идентификаторы задач</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete]
    public async Task DeleteAsync([FromBody] ICollection<Guid> ids, CancellationToken cancellationToken)
    {
        await userTaskService.DeleteRangeAsync(ids, cancellationToken);
    }
    
    /// <summary>
    /// Восстановление удаленной задачи
    /// </summary>
    /// <param name="id">Идентификатор удаленной задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPatch("{id:guid}/undelete")]
    public async Task UndeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {   
        await userTaskService.UndeleteAsync(id, cancellationToken);
    }
}