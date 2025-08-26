namespace ToDo.WebApi.Extensions;

/// <summary>
/// Настройки CORS
/// </summary>
public static class CorsExtension
{
    /// <summary>
    /// Регистрирует CORS
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection RegisterCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder
                    .WithOrigins("http://localhost:63342")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
        return services;
    }

    /// <summary>
    /// Регистрация CORS
    /// </summary>
    /// <param name="app">Построитель последовательности обработки запроса</param>
    /// <returns>Построитель последовательности обработки запроса</returns>
    public static IApplicationBuilder RegisterCors(this IApplicationBuilder app)
    {
        app.UseCors("AllowSpecificOrigin");
        
        return app;
    }
}