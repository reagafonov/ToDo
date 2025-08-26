using ToDo.WebApi.Abstractions.FiltersData;

namespace ToDo.WebApi.Repos.FiltersData;

/// <summary>
/// Набор данных для фильтрации UserTaskFilter
/// </summary>
public class UserTaskFilterData: BaseFilterData
{
    /// <summary>
    /// Фильтрация по id списка
    /// </summary>
    public Guid? UserTaskListId { get; set; }

    /// <summary>
    /// Фильтрация по части имени
    /// </summary>
    public string? NamePart { get; set; }

    /// <summary>
    /// Фильтрация по части описания
    /// </summary>
    public string? DescriptionPart { get; set; }
    
    /// <summary>
    /// Включать удаленные
    /// </summary>
    public bool WithDeleted { get; set; }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Набор идентификаторов
    /// </summary>
    public IEnumerable<Guid>? Ids { get; set; }
    
    ///<summary>
    /// Только удаленные задачи
    /// </summary>
    public bool DeletedOnly { get; set; }
    
    /// <summary>
    /// Поиск по текстовым полям
    /// </summary>
    public string TextSearch { get; set; }
}