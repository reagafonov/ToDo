using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDo.WebApi.Models;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Controllers;

/// <summary>
/// Контроллер файлов 
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class TodoFileController(IDocumentService fileService, ILogger<TodoFileController> logger, IMapper mapper):ControllerBase
{
    /// <summary>
    /// Получение данных о файле по идентификатору задачи
    /// </summary>
    /// <param name="taskId">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpGet("task/{taskId:guid}")]
    public async Task<List<UserTaskFileModel>> GetFileInfosAsync([FromRoute]Guid taskId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение файлов для задачи {taskId}", taskId);
        
        List<UserTaskFileSimpleDto> result = await fileService.GetFromTaskAsync(taskId, cancellationToken);
        
        return mapper.Map<List<UserTaskFileModel>>(result);
    }
    
    /// <summary>
    /// Загрузка файла по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpGet("{id}/contents")]
    //[ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK, "application/octet-stream")]
    public async Task<IActionResult> GetFileContentsAsync([FromRoute]string id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Загрузка файла {id}", id);
        
        UserTaskFileSimpleDto info = await fileService.GetInfoByIdAsync(id, cancellationToken);

        //без using
        Stream result = await fileService.GetContentsByIdAsync(id, cancellationToken);
        
        return File(result, contentType: "application/octet-stream", info.Name);
    }


    /// <summary>
    /// Загрузка файла на сервер
    /// </summary>
    /// <param name="taskId">Идентификатор задачи</param>
    /// <param name="file">Данные файла</param>
    /// <param name="name">Имя файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("{taskId:guid}")]
    public async Task<IActionResult> UploadAsync([FromRoute] Guid taskId, [FromForm]UploadFileRequestModel model, CancellationToken cancellationToken = default)
    {
        var file = model.File;
        var name = model.Name;
        
        if (file == null || file.Length == 0)
            return BadRequest("Файл не выбран");
        
        UserTaskFileDto? dto = mapper.Map<UserTaskFileDto>(file);
        dto.UserTaskId = taskId;
        dto.Name = name;
        
        await using Stream stream = file.OpenReadStream();
        
        dto.Contents = stream;
        
        string id = await fileService.AddAsync(dto, cancellationToken);
        
        return Ok(id);
    }

    /// <summary>
    /// Удаление файла по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id, CancellationToken cancellationToken = default)
    {
        await fileService.DeleteAsync(id, cancellationToken);
        
        return Ok();
    }
}