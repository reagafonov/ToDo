using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Abstractions.FiltersData;

namespace ToDo.WebApi.Repos.CommonFilters;

/// <summary>
/// Адаптер для постановки в очередь фильтра авторизации в цепочку базовых фильтров
/// </summary>
/// <param name="authorizationFilter">Фильтр авторизации определенный для сущности или общий</param>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TFilterData">Тип данных фильтра репозитория</typeparam>
public class AuthorizationCommonFilterAdapter<TEntity, TFilterData>(IAuthorizationFilter<TEntity> authorizationFilter):ICommonFilter<TEntity, TFilterData>
    where TEntity : BaseEntity
    where TFilterData : BaseFilterData
{
    /// <summary>
    /// Применяет зарегистрированный подфильтр авторизации
    /// </summary>
    /// <param name="filterData">Данные фильтра репозитория. Не используются</param>
    /// <param name="queryable">Запрос</param>
    /// <returns>Отфильтрованный запрос или null, если доступ к данным запрещен</returns>
    public IQueryable<TEntity>? ApplyPart(TFilterData filterData, IQueryable<TEntity> queryable) 
        => authorizationFilter.ApplyAuthorization(queryable);
}