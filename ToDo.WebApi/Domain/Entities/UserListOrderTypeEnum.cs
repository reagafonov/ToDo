namespace ToDo.WebApi.Domain.Entities;

/// <summary>
/// Способ сортировки задач списка
/// </summary>
public enum UserListOrderTypeEnum
{
    /// <summary>
    /// Не определено
    /// </summary>
    Undefined = 0,
    
    /// <summary>
    /// По приоритету
    /// </summary>
    Priority = 1,

    /// <summary>
    /// По дате создания
    /// </summary>
    CreationDate = 2,

    /// <summary>
    /// По дате выполнения
    /// </summary>
    CompletedDate = 3,

    /// <summary>
    /// По алфавиту
    /// </summary>
    Alphabetical = 4,
   
}