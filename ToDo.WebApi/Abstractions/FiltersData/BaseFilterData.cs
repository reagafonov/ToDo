namespace ToDo.WebApi.Abstractions.FiltersData;

/// <summary>
/// Общие данные фильтра
/// </summary>
public class BaseFilterData
{
    /// <summary>
    /// Страница
    /// </summary>
    /// <remarks>
    ///Если страница пустая то, если включена пагинация, то считается первой
    /// </remarks>
    public int? Page { get; init; }
    
    /// <summary>
    /// Размер страницы
    /// </summary>
    /// <remarks>Если не установлена, то пагинация выключена</remarks>
    public int? PageSize { get; init; }
    
    /// <summary>
    /// Включать удаленные
    /// </summary>
    public bool WithDeleted { get; init; }
}