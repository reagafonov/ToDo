namespace ToDo.WebApi.Models;

/// <summary>
/// Данные списка задач для добавления
/// </summary>
public record UserTaskListAddModel
{
    /// <summary>
    /// Имя
    /// </summary>
    /// <remarks>Обязательно</remarks>
    public required string Name { get; set; }
}