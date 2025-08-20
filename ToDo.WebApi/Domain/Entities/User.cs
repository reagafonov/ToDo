using Microsoft.EntityFrameworkCore;
using ToDo.WebApi.Abstractions;

namespace ToDo.WebApi.Domain.Entities;

/// <summary>
/// Данные пользователя после авторизации
/// </summary>
[Index(nameof(User.Username), IsUnique = true)]
public class User:BaseEntity
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Username { get; init; }
    
    /// <summary>
    /// Email
    /// </summary>
    public required string Password { get; init; }
    
}