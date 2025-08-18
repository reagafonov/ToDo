using ToDo.Components;
using  Microsoft.AspNetCore.Builder;
namespace ToDo.Extensions;

/// <summary>
/// Настройка расширений blazor
/// </summary>
public static class BlazorExtensions
{
    /// <summary>
    /// Настройка сервиоов Blazor
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection ConfigureBlazor(this IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents();
        return services;
    }

    /// <summary>
    /// Регистрация middleware для blazor
    /// </summary>
    /// <param name="app">Построитель путей запроса</param>
    /// <returns>Построитель путей запроса</returns>
    public static IEndpointRouteBuilder ConfigureBlazor(this IEndpointRouteBuilder app)
    {
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        return app;
    }
}