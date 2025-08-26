using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDo.WebApi.Domain;

namespace ToDo.WebApi.Middleware;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is KeyNotFoundException)
        {
            context.Result = new NotFoundObjectResult(new ErrorResponse
            {
                Type = "https://api.example.com/errors/key-not-found",
                Title = "Key not found",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status404NotFound
            });
            context.ExceptionHandled = true;
        }
    }
}