using System.Security.Claims;
using ToDo.WebApi.ServiceAbstractions;

namespace ToDo.WebApi.Services;

/// <summary>
/// Реализация сервиса пользователей для одного пользователя
/// </summary>
public class DefaultUserService:IUserService
{
    /// <summary>
    /// Возвращает всегда одно и то же
    /// </summary>
    /// <param name="user">Данные пользователя, если есть</param>
    /// <returns>Идкнтификатор текущего пользователя</returns>
    public Task<string> GetCurrentUserIdAsync(ClaimsPrincipal? user)
    {
        return Task.FromResult("test");
    }
}