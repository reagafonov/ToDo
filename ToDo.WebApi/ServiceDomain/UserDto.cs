namespace ToDo.WebApi.ServiceDomain;

/// <summary>
/// Дто пользователя
/// </summary>
public class UserDto
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