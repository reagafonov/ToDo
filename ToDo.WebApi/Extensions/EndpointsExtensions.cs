using ToDo.WebApi.Middleware;

namespace ToDo.WebApi.Extensions;

/// <summary>
/// Работа с контроллерами
/// </summary>
public static class EndpointsExtensions
{
    /// <summary>
    /// Добавляет настройки контроллеров
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection RegisterControllers(this IServiceCollection services)
    {
        services.AddControllers();
        
        return services;
    }
    
    /// <summary>
    /// Регистрация контроллера
    /// </summary>
    /// <param name="app">Построитель последовательности обработки запроса</param>
    /// <returns>Построитель последовательности обработки запроса</returns>
    public static IApplicationBuilder RegisterControllers(this IApplicationBuilder app)
    {
        // Регистрирует обработчики исключений в конвеере middleware
        app.UseMiddleware<ErrorHandlingMiddleware>();
        
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        return app;
    }
}