using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace ToDo.WebApi.Extensions;

/// <summary>
/// Настройка логгеров
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Настройка Serilog из конфигурации
    /// </summary>
    /// <param name="services">Билдер хоста</param>
    /// <returns>Билдер хоста </returns>
    /// <example>builder.Host.RegisterSeq()</example>
    public static IHostBuilder RegisterSerilogConfig(this IHostBuilder builder)
    {
        builder.UseSerilog((context, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(context.Configuration));
        
        return builder;
    }
    
    /// <summary>
    /// Настройка Serilog для консоли
    /// </summary>
    /// <param name="services">Билдер хоста</param>
    /// <returns>Билдер хоста </returns>
    /// <example>builder.Host.RegisterSeq()</example>
    public static IHostBuilder RegisterSerilogConsole(this IHostBuilder builder)
    {
        builder.UseSerilog((context, loggerConfiguration) =>
            loggerConfiguration.WriteTo.Console(theme: AnsiConsoleTheme.Literate));
        
        return builder;
    }
}