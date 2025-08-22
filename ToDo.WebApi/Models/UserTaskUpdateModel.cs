using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.Models;

/// <summary>
/// Данные задачи для обновления
/// </summary>
public class UserTaskUpdateModel
{
    /// <summary>
    /// Имя
    /// </summary>
    /// <remarks>Обязательно</remarks>
    public required string Name { get; init; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; init; }
    
    /// <summary>
    /// Идентификатор списка задач
    /// </summary>
    public Guid? UserTaskListId { get; set; }
    
    /// <summary>
    /// Признак выполнения
    /// </summary>
    public string IsCompleted { get; set; }
    
    /// <summary>
    /// Приоритет
    /// </summary>
    public string? Priority { get; set; }
}