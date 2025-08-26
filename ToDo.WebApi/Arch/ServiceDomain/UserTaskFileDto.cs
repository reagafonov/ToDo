namespace ToDo.WebApi.ServiceDomain;

/// <summary>
/// Дто подключаемого к задаче файла
/// </summary>
public class UserTaskFileDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid UserTaskId { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Содержание
    /// </summary>
    public required Stream Contents { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime Created { get; set; }
}