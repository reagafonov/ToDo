namespace ToDo.Models;

/// <summary>
/// Возвращаемые данные для списка задач
/// </summary>
public class UserTaskListModel:BaseOutputModel
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
    /// Признак удаления
    /// </summary>
    public bool IsDeleted { get; set; }
}