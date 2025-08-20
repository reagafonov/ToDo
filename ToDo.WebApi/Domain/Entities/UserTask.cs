using System.ComponentModel.DataAnnotations.Schema;
using ToDo.WebApi.Abstractions;

namespace ToDo.WebApi.Domain.Entities;

/// <summary>
/// Задача
/// </summary>
public class UserTask:BaseOwnerEntity
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
    /// Список задач в котором находится задача
    /// </summary>
    [ForeignKey(nameof(UserTaskListId))]
    public UserTaskList TypeUserTaskList { get; set; }

    /// <summary>
    /// Признак выполнения
    /// </summary>
    public bool IsCompleted { get; set; }
}