namespace ToDo.WebApi.ServiceDomain;

/// <summary>
/// Дто задачи
/// </summary>
public class UserTaskDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Признак выполнения
    /// </summary>
    public bool IsCompleted { get; set; }
    
    /// <summary>
    /// Признак удаления
    /// </summary>
    public bool IsDeleted { get; set; }
}