namespace ToDo.WebApi.Domain.Entities;

/// <summary>
/// Данные пользователя после авторизации
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
}