using System.Security.Claims;

namespace ToDo.WebApi.Extensions;

/// <summary>
/// Расширения для работы с данными пользователей
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Получение идентификатора пользователея из Jwt
    /// </summary>
    /// <param name="principal">Данные о пользователе</param>
    /// <returns>Идентификатор пользователя, если есть</returns>
    public static string? GetUserId(this ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
    }
}