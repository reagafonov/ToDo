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
    public required string Name { get; set; }
    
    /// <summary>
    /// Тип сортировки
    /// </summary>
    public UserListOrderTypeEnum OrderType { get; set; }
    
    /// <summary>
    /// Направление сортировки
    /// </summary>
    public OrderDirectionEnum OrderDirection { get; set; }
}