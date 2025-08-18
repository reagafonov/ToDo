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
    public string Name { get; set; }
    
    /// <summary>
    /// Создержание
    /// </summary>
    public byte[] Contents { get; set; }
}