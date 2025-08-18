using Microsoft.AspNetCore.Authorization;
using ToDo.WebApi.Domain;
using ToDo.WebApi.Middleware;

namespace ToDo.WebApi.Extensions;

/// <summary>
/// Реализует расширения авторизацуии
/// </summary>
public static class AuthorizationExtension
{
    /// <summary>
    /// Регистрирует политики авторизации
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection RegiserAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("EntityAccess", policy => policy.Requirements.Add(new EntityAccessRequirement()));
        });

        services.AddTransient<IAuthorizationHandler, EntityAccessHandler>();
        
        return services;
    }
}