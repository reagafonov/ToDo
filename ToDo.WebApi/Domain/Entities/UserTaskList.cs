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
    public string Name { get; set; }

    /// <summary>
    /// Задачи списка
    /// </summary>
    public ICollection<UserTask> UserTasks { get; set; }
   
    /// <summary>
    /// Тип сортировки
    /// </summary>
    public UserListOrderTypeEnum OrderType { get; set; }
    
    /// <summary>
    /// Направление сортировки
    /// </summary>
    public OrderDirectionEnum OrderDirection { get; set; }
    
}