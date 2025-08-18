using Microsoft.EntityFrameworkCore;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.CommonFilters;
using ToDo.WebApi.Repos.FiltersData;

namespace ToDo.WebApi.Repos.UserTasks;

public class UserTaskFilter(IEnumerable<ICommonFilter<UserTask, UserTaskFilterData>> partialFilters) 
    :BaseFilter<UserTask, UserTaskFilterData>(partialFilters)
{

    public  override IQueryable<UserTask>? Apply(UserTaskFilterData filterData, IQueryable<UserTask> queryable)
    {
        if (filterData == null)
            throw new ArgumentNullException(nameof(filterData));

        if (filterData.WithDeleted)
            queryable = queryable.IgnoreQueryFilters();
        
        if (!string.IsNullOrWhiteSpace(filterData.DescriptionPart))
            queryable = queryable.Where(t => t.Description.Contains(filterData.DescriptionPart));
        
        if (!string.IsNullOrWhiteSpace(filterData.NamePart))
            queryable = queryable.Where(t => t.Name.Contains(filterData.NamePart));
        
        if (filterData.UserTaskListId.HasValue)
            queryable = queryable.Where(t => t.UserTaskListId == filterData.UserTaskListId);
        
        if (filterData.Id.HasValue)
            queryable = queryable.Where(t => t.Id == filterData.Id);
        
        return base.Apply(filterData, queryable);
    }
}