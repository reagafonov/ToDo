using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.CommonFilters;
using ToDo.WebApi.Repos.FiltersData;

namespace ToDo.WebApi.Repos.UserTaskLists;

/// <summary>
/// Фильтр по спискам задач
/// </summary>
/// <param name="partialFilters">Фильтр запроса по задаче</param>
public class UserTaskListFilter(IEnumerable<ICommonFilter<UserTaskList, UserTaskListFilterData>> partialFilters) 
    :BaseFilter<UserTaskList, UserTaskListFilterData>(partialFilters);