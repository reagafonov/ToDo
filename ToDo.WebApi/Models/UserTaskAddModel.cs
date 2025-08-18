namespace ToDo.WebApi.Models;

/// <summary>
/// Данные задачи для добавления
/// </summary>
public class UserTaskAddModel
{
    /// <summary>
    /// Имя
    /// </summary>
    /// <remarks>Обязательно</remarks>
    public required string Name { get; init; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; init; }
}