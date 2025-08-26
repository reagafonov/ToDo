using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.Models;

/// <summary>
/// Возвращаемые данные задачи
/// </summary>
public class UserTaskModel:BaseOutputModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    /// <remarks>Обязательно</remarks>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    /// <remarks>Обязательно</remarks>
    public required string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; init; }
    
    /// <summary>
    /// Признак выполнения
    /// </summary>
    public bool IsCompleted { get; set; }
    
    /// <summary>
    /// Приоритет
    /// </summary>
    public UserTaskPriorityEnum? Priority { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Дата выполнения
    /// </summary>
    public DateTime? CompleteDate { get; set; }
    
    /// <summary>
    /// Идентификатор списка задач
    /// </summary>
    public Guid? UserTaskListId { get; set; }

}