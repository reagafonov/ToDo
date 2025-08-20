using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ToDo.WebApi.Keycloak.Extensions;

/// <summary>
/// Расширения для работы с авторизацией/аутентификацией Keycloak
/// </summary>
public static class KeycloakAuthenticationExtension
{
    /// <summary>
    /// Настройка получения данных аутентификации через keycloak
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    /// <param name="configuration">Конфигурация</param>
    public static IServiceCollection RegisterKeycloakAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration["Keycloak:Authority"];
                options.ClientId = configuration["Keycloak:ClientId"];
                options.ClientSecret = configuration["Keycloak:ClientSecret"];
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
            });
        
        services.AddAuthorization();
        
        return services;
    }

    /// <summary>
    /// Настройка получения данных аутентификации через keycloak
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseKeycloakAuthentication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }
}