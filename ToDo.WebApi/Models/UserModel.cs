namespace ToDo.WebApi.Models;

/// <summary>
/// Дто пользователя
/// </summary>
public class UserModel
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Username { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}