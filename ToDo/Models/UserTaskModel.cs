namespace ToDo.Models;

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
    
    
}