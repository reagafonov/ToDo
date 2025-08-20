using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.Models;

/// <summary>
/// Возвращаемые сокращенные данные задачи для списков
/// </summary>
public class UserTaskSmallModel:BaseOutputModel
{
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Признак выполненности
    /// </summary>
    public required bool IsCompleted { get; set; }
    
    /// <summary>
    /// Пизнак удаленности
    /// </summary>
    public required bool IsDeleted { get; init; }
    
    /// <summary>
    /// Приоритет
    /// </summary>
    public UserTaskPriority? Priority { get; set; }
    
    /// <summary>
    /// Порядок вывода
    /// </summary>
    public int Order { get; set; }
}