using Microsoft.AspNetCore.Authorization;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Domain;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.ServiceAbstractions;

namespace ToDo.WebApi.Middleware;

/// <summary>
/// Регулирует дотуп к сущностям
/// </summary>
public class EntityAccessHandler(IUserService userService): AuthorizationHandler<EntityAccessRequirement>
{
    /// <summary>
    /// Проверка дот
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requirement"></param>
    /// <exception cref="UnauthorizedAccessException"></exception>
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EntityAccessRequirement requirement)
    {
        if (context.Resource == null || context.Resource is not BaseEntity entity)
            return;
        
        if (entity.OwnerUserId != await userService.GetCurrentUserIdAsync(context.User))
            throw new UnauthorizedAccessException();
        
        context.Succeed(requirement);
    }
}