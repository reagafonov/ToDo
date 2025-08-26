using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Не задано название.", AllowEmptyStrings = false)]
    [MaxLength(100)]
    public required string Name { get; init; }

    /// <summary>
    /// Описание
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; init; }
    
    /// <summary>
    /// Идентификатор списка задач
    /// </summary>
    [Required(ErrorMessage = "Не задан идентификатор списка")]
    public Guid UserTaskListId { get; set; }
}