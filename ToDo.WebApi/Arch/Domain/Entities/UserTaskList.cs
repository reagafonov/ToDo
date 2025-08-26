using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ToDo.WebApi.Abstractions;

namespace ToDo.WebApi.Domain.Entities;

/// <summary>
/// Список пользователей
/// </summary>
public class UserTaskList:BaseEntity
{
    /// <summary>
    /// Название списка
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Не задано имя списка")]
    [MaxLength(100, ErrorMessage = "Длина имени не должна превышать 100 символов")]
    public string Name { get; set; }

    /// <summary>
    /// Задачи списка
    /// </summary>
    public ICollection<UserTask> UserTasks { get; set; }

    /// <summary>
    /// Тип сортировки
    /// </summary>
    public UserListOrderTypeEnum OrderType { get; set; } = UserListOrderTypeEnum.Undefined;

    /// <summary>
    /// Направление сортировки
    /// </summary>
    public OrderDirectionEnum OrderDirection { get; set; } = OrderDirectionEnum.Ascending;

}