using System.ComponentModel;
using System.Reflection;

namespace ToDo.WebApi.Domain.Entities;

/// <summary>
/// Приоритет задачи
/// </summary>
[DefaultValue(Medium)]
public enum UserTaskPriorityEnum
{
    /// <summary>
    /// Низкий
    /// </summary>
    Low = 1,

    /// <summary>
    /// Средний
    /// </summary>
   
    Medium = 2,

    /// <summary>
    /// Высокий
    /// </summary>
    High = 3,
}