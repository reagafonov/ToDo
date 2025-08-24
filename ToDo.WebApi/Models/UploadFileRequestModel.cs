using Microsoft.AspNetCore.Mvc;

namespace ToDo.WebApi.Models;

/// <summary>
/// Модель для загрузки файлов
/// </summary>
public class UploadFileRequestModel
{
    /// <summary>
    /// Содержимое файла
    /// </summary>
    [FromForm(Name="file")] public IFormFile File { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [FromForm(Name = "name")]public string Name { get; set; }
}