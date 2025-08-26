using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Не задано имя файла", AllowEmptyStrings = false)]
    [MaxLength(200)]
    public required string Name { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime Created { get; set; }
    
}