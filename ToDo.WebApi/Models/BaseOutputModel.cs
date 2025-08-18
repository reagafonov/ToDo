namespace ToDo.WebApi.Models;

/// <summary>
/// Базовая модель
/// </summary>
public class BaseOutputModel
{
    /// <summary>
    /// Является ли удаленным
    /// </summary>
    public bool IsDeleted { get; set; }
}