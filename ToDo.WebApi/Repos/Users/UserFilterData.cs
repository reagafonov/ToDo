using ToDo.WebApi.Abstractions.FiltersData;

namespace ToDo.WebApi.Repos.Users;

/// <summary>
/// Фильтр по пользователям
/// </summary>
public class UserFilterData:BaseFilterData
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Username { get; set; }
}