using System.ComponentModel.DataAnnotations;

namespace ToDo.WebApi.Abstractions;

/// <summary>
/// Общие свойства сущностей
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Признак удаления
    /// </summary>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public string OwnerUserId { get; set; }
}