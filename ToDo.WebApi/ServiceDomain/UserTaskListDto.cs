using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.ServiceDomain;

/// <summary>
/// Дто списка пользователей
/// </summary>
public class UserTaskListDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public string OwnerUserId { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Тип сортировки
    /// </summary>
    public UserListOrderTypeEnum OrderType { get; set; }
    
    /// <summary>
    /// Направление сортировки
    /// </summary>
    public OrderDirectionEnum OrderDirection { get; set; }
}