using Microsoft.EntityFrameworkCore;
using ToDo.WebApi.Domain;

namespace ToDo.WebApi.Extensions;

/// <summary>
/// Расширения для регистрации EntityFramework
/// </summary>
public static class EntityFrameworkExtensions
{
    /// <summary>
    /// Регистрация сервисоов EntityFramework
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    /// <param name="configuration">Конфигурация</param>
    public static IServiceCollection RegisterEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<DataContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString), ServiceLifetime.Singleton);
        
        return services;
    }

    /// <summary>
    /// Автоматически применяет миграции
    /// </summary>
    /// <param name="app">Построитель последовательности обработки запроса</param>
    /// <returns>Построитель последовательности обработки запроса</returns>
    public static IApplicationBuilder AutoApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.Migrate();

        return app;
    }

}