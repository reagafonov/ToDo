namespace ToDo.WebApi.ServiceDomain;

/// <summary>
/// Получение данных о файле без содержания
/// </summary>
public class UserTaskFileSimpleDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid UserTaskId { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime Created { get; set; }

}