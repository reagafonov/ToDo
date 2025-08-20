namespace ToDo.WebApi.Jwt.Options;

/// <summary>
/// Данные генерации токена Jwt
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Кто выпустил
    /// </summary>
    public string Issuer { get; set; }
    
    /// <summary>
    /// Для кого
    /// </summary>
    public string Audience { get; set; }
    
    /// <summary>
    /// Ключ
    /// </summary>
    public string SecurityKey { get; set; }
}