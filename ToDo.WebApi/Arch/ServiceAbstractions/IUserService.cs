using System.Security.Claims;
using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.ServiceAbstractions;

/// <summary>
/// Сервис данных пользователя
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получает идентификатор текущего пользователя
    /// </summary>
    /// <param name="user">Данные пользователя, если есть</param>
    /// <returns>Идентификатор текущего пользователя</returns>
    Task<string> GetCurrentUserIdAsync(ClaimsPrincipal? user);
}