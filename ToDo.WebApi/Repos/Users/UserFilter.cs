using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.CommonFilters;

namespace ToDo.WebApi.Repos.Users;

/// <summary>
/// Фильтр по пользователям
/// </summary>
/// <param name="partialFilters"></param>
public class UserFilter(IEnumerable<ICommonFilter<User, UserFilterData>> partialFilters) :BaseFilter<User, UserFilterData>(partialFilters)
{
    /// <summary>
    /// Применяет фильтр по пользователям к запросу пользователей 
    /// </summary>
    /// <param name="filterData">данные фильтра</param>
    /// <param name="queryable">запрос</param>
    /// <returns>Отфильтрованный запрос</returns>
    public override IQueryable<User> Apply(UserFilterData filterData, IQueryable<User> queryable)
    {
        if (string.IsNullOrWhiteSpace(filterData.Username))
            queryable = queryable.Where(u => u.Username == filterData.Username);
        return base.Apply(filterData, queryable);
    }
}