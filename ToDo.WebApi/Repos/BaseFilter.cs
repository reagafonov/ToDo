using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Abstractions.FiltersData;
using ToDo.WebApi.Repos.CommonFilters;

namespace ToDo.WebApi.Repos;

/// <summary>
/// Общий фильтр для всех сущностей, применяет подфильтры и пагинацию
/// </summary>
/// <param name="partialFilters">Подфильтры</param>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TFilterData">Тип данных фильтра репозитория</typeparam>
public class BaseFilter<TEntity, TFilterData>(IEnumerable<ICommonFilter<TEntity,TFilterData>> partialFilters):IFilter<TEntity, TFilterData> 
    where TEntity:BaseEntity
    where TFilterData:BaseFilterData
{
    public virtual IQueryable<TEntity> Apply(TFilterData filterData, IQueryable<TEntity> queryable)
    {
        foreach (ICommonFilter<TEntity, TFilterData> partialFilter in partialFilters)
        {
            IQueryable<TEntity>? partialResult = partialFilter.ApplyPart(filterData, queryable);
            if (partialResult is null)
                throw new KeyNotFoundException();
            queryable = partialResult;
        }
        
        if (filterData.PageSize is null or <= 0)
            return queryable;
        
        int filterDataPage = filterData.Page ?? 1;
        int filterDataPageSize = filterData.PageSize!.Value;
        int filterDataSkipCount = (filterDataPage - 1) * filterDataPageSize;
        
        queryable = queryable.Skip(filterDataSkipCount).Take(filterDataPageSize);
        
        return queryable;
    }
}