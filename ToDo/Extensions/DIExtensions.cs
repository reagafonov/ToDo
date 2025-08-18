using Microsoft.Extensions.Options;
using ToDo.Options;

namespace ToDo.Extensions;

/// <summary>
/// Расширения IoC
/// </summary>
public static class DiExtensions
{


    /// <summary>
    /// Регистрирует вспомогательные сервисы
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection RegisterMisc(this IServiceCollection services)
    {
        services.AddTransient<HttpClient>(services =>
        {
            ServicesOptions serviceOptions = services.GetRequiredService<IOptions<ServicesOptions>>().Value ??
                                             throw new InvalidDataException("В конфигурации не найдена настройка для Services");
            
            string webApi = string.IsNullOrWhiteSpace(serviceOptions.WebApi) ? throw new InvalidDataException($"В конфигурации не найдена настройка для Services.{nameof(serviceOptions.WebApi)}")
                : serviceOptions.WebApi;
            
            return new HttpClient
            {
                BaseAddress = new Uri(webApi+"/v1/")
            };
        });

        return services;
    }

    /// <summary>
    /// Конфигурация IOptions
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <param name="configuration">Конфигурация</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServicesOptions>(configuration.GetSection("Services"));
        return services;
    }


}