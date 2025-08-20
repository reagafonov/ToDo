namespace ToDo.WebApi.Models;

/// <summary>
/// Данные о загруженном файле
/// </summary>
public record UserTaskFileModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }

    
}