namespace ToDo.Models;

/// <summary>
/// Данные списка задач для добавления
/// </summary>
public class UserTaskListAddModel
{
    /// <summary>
    /// Имя
    /// </summary>
    /// <remarks>Обязательно</remarks>
    public required string Name { get; set; }
}