using System.ComponentModel.DataAnnotations;
using ToDo.WebApi.Domain.Entities;

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
    [Required(ErrorMessage = "Не задано имя списка")]
    public required string Name { get; set; }
    
    /// <summary>
    /// Тип сортировки
    /// </summary>
    public string? OrderType { get; set; }
    
    /// <summary>
    /// Направление сортировки
    /// </summary>
    public string? OrderDirection { get; set; }
}