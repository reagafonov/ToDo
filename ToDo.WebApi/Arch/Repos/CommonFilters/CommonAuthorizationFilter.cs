using System.Security.Claims;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Abstractions.FiltersData;
using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.Repos.CommonFilters;

/// <summary>
/// Базовый фильтр данных по пользователям и ролям
/// </summary>
/// <param name="httpContextAccessor">Контекст</param>
/// <typeparam name="TEntity">Сущность</typeparam>
public class CommonAuthorizationFilter<TEntity>(IHttpContextAccessor httpContextAccessor)
    : IAuthorizationFilter<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Данные пользователя, залогиненного в систему
    /// </summary>
    private readonly ClaimsPrincipal _user = httpContextAccessor.HttpContext.User;

    /// <summary>
    /// Логика по умолчанию для авторизации сущности
    /// </summary>
    /// <param name="queryable">Запрос</param>
    /// <returns>Отфильтрованный запрос или null</returns>
    public IQueryable<TEntity>? ApplyAuthorization(IQueryable<TEntity> queryable)
    {
        if (!_user?.Identity?.IsAuthenticated ?? false)
            return null;
        
        if (_user.IsInRole("Admin"))
            return queryable; 
        
        string? userId = _user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        queryable = queryable.Where(entity => entity.OwnerUserId == userId);

        return queryable;
    }
}