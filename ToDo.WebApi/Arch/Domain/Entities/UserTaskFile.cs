using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDo.WebApi.Abstractions;

namespace ToDo.WebApi.Domain.Entities;

/// <summary>
/// Файлы
/// </summary>
public class UserTaskFile:BaseEntity
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid UserTaskId { get; set; }
    
    /// <summary>
    /// Задача
    /// </summary>
    [ForeignKey(nameof(UserTaskId))]
    public UserTask UserTask { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [MaxLength(200)]
    public string Name { get; set; }
    
    /// <summary>
    /// Создержание
    /// </summary>
    [MaxLength(length:16*1024*1024)]
    public byte[] Contents { get; set; }
}