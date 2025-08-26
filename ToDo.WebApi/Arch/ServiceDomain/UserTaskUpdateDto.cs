using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.ServiceDomain;

/// <summary>
/// Дто задачи
/// </summary>
public class UserTaskUpdateDto
{
   /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Идентификатор списка задач
    /// </summary>
    public Guid? UserTaskListId { get; set; }
    
    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public string OwnerUserId { get; set; }
    
    /// <summary>
    /// Признак выполнения
    /// </summary>
    public bool IsCompleted { get; set; }
    
    /// <summary>
    /// Признак удаления
    /// </summary>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Приоритет
    /// </summary>
    public UserTaskPriorityEnum? Priority { get; set; }

}