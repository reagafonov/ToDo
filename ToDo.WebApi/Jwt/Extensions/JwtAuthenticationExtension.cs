using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ToDo.WebApi.Jwt.Extensions;

/// <summary>
/// Расширения для работы с авторизацией/аутентификацией JwtBearer
/// </summary>
public static class JwtAuthenticationExtension
{
    /// <summary>
    /// Настройка получения данных аутентификации через keycloak
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    /// <param name="configuration">Конфигурация</param>
    public static IServiceCollection RegisterJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                };
            });
        
        services.AddAuthorization();
        
        return services;
    }

    /// <summary>
    /// Настройка получения данных аутентификации через keycloak
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }

    /// <summary>
    /// Добавляет конфигурацию для Jwt
    /// </summary>
    /// <param name="configurationBuilder">Изменяемая конфигурация</param>
    /// <returns></returns>
    public static IConfigurationManager AddJwtConfigurationSources(this IConfigurationManager configurationBuilder)
    {
        configurationBuilder.AddJsonFile("appsettings.Jwt.json");
        return configurationBuilder;
    }
}