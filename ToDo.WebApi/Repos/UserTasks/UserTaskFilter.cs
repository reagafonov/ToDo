using Microsoft.EntityFrameworkCore;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.CommonFilters;
using ToDo.WebApi.Repos.FiltersData;

namespace ToDo.WebApi.Repos.UserTasks;
/// <summary>
/// Фильтр задач
/// </summary>
/// <param name="partialFilters">Подфильтры</param>
public class UserTaskFilter(IEnumerable<ICommonFilter<UserTask, UserTaskFilterData>> partialFilters) 
    :BaseFilter<UserTask, UserTaskFilterData>(partialFilters)
{

    /// <summary>
    /// Применение фильтра к запросу
    /// </summary>
    /// <param name="filterData">Данные фильтра</param>
    /// <param name="queryable">Запрос</param>
    /// <returns>Отфильтрованный запрос</returns>
    /// <exception cref="ArgumentNullException">Если данные отсутствуют</exception>
    public  override IQueryable<UserTask> Apply(UserTaskFilterData filterData, IQueryable<UserTask> queryable)
    {
        if (filterData == null)
            throw new ArgumentNullException(nameof(filterData));

        if (filterData.WithDeleted)
            queryable = queryable.IgnoreQueryFilters();
        
        if (!string.IsNullOrWhiteSpace(filterData.DescriptionPart))
            queryable = queryable.Where(t => (t.Description ?? "").Contains(filterData.DescriptionPart));
        
        if (!string.IsNullOrWhiteSpace(filterData.NamePart))
            queryable = queryable.Where(t => t.Name.Contains(filterData.NamePart));
        
        if (filterData.UserTaskListId.HasValue)
            queryable = queryable.Where(t => t.UserTaskListId == filterData.UserTaskListId);
        
        if (filterData.Id.HasValue)
            queryable = queryable.Where(t => t.Id == filterData.Id);
        
        if (filterData.Ids != null && filterData.Ids.Any())
            queryable = queryable.Where(t => filterData.Ids.Contains(t.Id));

        if (filterData.DeletedOnly)
            queryable = queryable.Where(t => t.IsDeleted);
        
        if (!string.IsNullOrWhiteSpace(filterData.TextSearch))
            queryable = queryable.Where(t => t.Name.Contains(filterData.TextSearch) || (t.Description ?? "").Contains(filterData.TextSearch));
        
        return base.Apply(filterData, queryable);
    }
}