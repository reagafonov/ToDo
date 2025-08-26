using System.ComponentModel.DataAnnotations;
using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.Models;

/// <summary>
/// Данные задачи для обновления
/// </summary>
public class UserTaskUpdateModel
{
    /// <summary>
    /// Имя
    /// </summary>
    /// <remarks>Обязательно</remarks>
    [MaxLength(100,ErrorMessage = "Длина имени не должна превышать 100")]
    public required string Name { get; init; }

    /// <summary>
    /// Описание
    /// </summary>
    [MaxLength(1000,ErrorMessage = "Длина описания не должна превышать 1000")]
    public string? Description { get; init; }
    
    /// <summary>
    /// Идентификатор списка задач
    /// </summary>
    public Guid? UserTaskListId { get; set; }
    
    /// <summary>
    /// Признак выполнения
    /// </summary>
    public string IsCompleted { get; set; }
    
    /// <summary>
    /// Приоритет
    /// </summary>
    public string? Priority { get; set; }
}