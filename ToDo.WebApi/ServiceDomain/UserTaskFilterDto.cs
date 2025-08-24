namespace ToDo.WebApi.ServiceDomain;

/// <summary>
/// Часть фильтра задач доступная из сервиса
/// </summary>
public struct UserTaskFilterDto
{
    /// <summary>
    /// Идентификатор списка
    /// </summary>
    public Guid UserTaskListId { get; set; }

    /// <summary>
    /// Фильтр по части имени
    /// </summary>
    public string? NamePart { get; set; }

    /// <summary>
    /// Фильтр по части описания
    /// </summary>
    public string DescriptionPart { get; set; }

    /// <summary>
    /// Номер страницы
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Включать удаленные
    /// </summary>
    public bool WithDeleted { get; set; }

    /// <summary>
    /// Только удаленные задачи
    /// </summary>
    public bool DeletedOnly { get; set; }
}