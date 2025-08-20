using Microsoft.AspNetCore.Authorization;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Domain;
using ToDo.WebApi.ServiceAbstractions;

namespace ToDo.WebApi.Middleware;

/// <summary>
/// Регулирует доступ к сущностям
/// </summary>
public class EntityAccessHandler(IUserServiceClaims userServiceClaims): AuthorizationHandler<EntityAccessRequirement>
{
    /// <summary>
    /// Проверка доступа
    /// </summary>
    /// <param name="context">Контекст авторизации</param>
    /// <param name="requirement">Определение требования</param>
    /// <exception cref="UnauthorizedAccessException">Если нет доступа</exception>
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EntityAccessRequirement requirement)
    {
        if (context.Resource == null || context.Resource is not BaseOwnerEntity entity)
            return;
        
        if (entity.OwnerUserId != await userServiceClaims.GetCurrentUserIdAsync(context.User))
            throw new UnauthorizedAccessException();
        
        context.Succeed(requirement);
    }
}