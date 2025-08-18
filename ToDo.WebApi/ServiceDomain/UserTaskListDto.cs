namespace ToDo.WebApi.ServiceDomain;

/// <summary>
/// Дто списка пользователей
/// </summary>
public class UserTaskListDto
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
    /// Идентификатор владельца
    /// </summary>
    public string OwnerUserId { get; set; }
}