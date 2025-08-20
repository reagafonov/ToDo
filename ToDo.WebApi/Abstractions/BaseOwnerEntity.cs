namespace ToDo.WebApi.Abstractions;

/// <summary>
/// Хранит идентификатор владельца
/// </summary>
public abstract class BaseOwnerEntity:BaseEntity
{
    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public string OwnerUserId { get; set; }
}