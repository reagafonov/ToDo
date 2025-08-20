using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using ToDo.WebApi.Controllers;
using ToDo.WebApi.Domain;

namespace ToDo.WebApi.Middleware;

/// <summary>
/// Возвращает коды вместо ошибкок
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="next"/>Следующий обработчик в цепочке, там должна быть бизнес логика/param>
    /// <param name="logger">логгер</param>
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Перехват исключений
    /// </summary>
    /// <param name="context">Контекст</param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, ex.Message);
            await Handle404ExceptionAsync(context, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, ex.Message);
            await HandleUnauthorizedExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await Handle5xxExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Вывод для ошибки 404
    /// </summary>
    /// <param name="context">контекст</param>
    /// <param name="ex">Полученное исключение</param>
    /// <returns></returns>
    private static Task Handle404ExceptionAsync(HttpContext context, Exception ex)
    {
        int code = StatusCodes.Status404NotFound;
        ErrorResponse result = new ErrorResponse
        {
            Type = "https://api.example.com/errors/key-not-found",
            Title = "Key not found",
#if DEBUG
            Detail = ex.Message,
#else
            Detail = "Информация скрыта"
#endif            
            Status = code
        };

        context.Response.StatusCode = code;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
    
    /// <summary>
    /// Вывод для ошибки 5xx
    /// </summary>
    /// <param name="context">контекст</param>
    /// <param name="ex">Полученное исключение</param>
    /// <returns></returns>
    private static Task Handle5xxExceptionAsync(HttpContext context, Exception ex)
    {
        int code = StatusCodes.Status500InternalServerError;
        ErrorResponse result = new ErrorResponse
        {
            Type = "https://api.example.com/errors/key-not-found",
            Title = "Ошибка на сервере",
#if DEBUG
            Detail = ex.Message,
#else
            Detail = "Информация скрыта"
#endif         
            Status = code
        };

        context.Response.StatusCode = code;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
    
    /// <summary>
    /// Вывод для неавторизованных данных
    /// </summary>
    /// <param name="context">контекст</param>
    /// <param name="ex">Полученное исключение</param>
    /// <returns></returns>
    private static Task HandleUnauthorizedExceptionAsync(HttpContext context, Exception ex)
    {
        int code = StatusCodes.Status401Unauthorized;
        ErrorResponse result = new ErrorResponse
        {
            Type = "https://api.example.com/errors/key-not-found",
            Title = "Ошибка на сервере",
#if DEBUG
            Detail = ex.Message,
#else
            Detail = "Информация скрыта"
#endif         
            Status = code
        };

        context.Response.StatusCode = code;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
}