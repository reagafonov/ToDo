using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDo.WebApi.Jwt.Options;
using ToDo.WebApi.Models;
using ToDo.WebApi.Options;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Controllers;

/// <summary>
/// Контроллер доступа
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
public class AuthController(IUserService _userService, IMapper mapper, IOptions<JwtOptions> options) : ControllerBase
{
    private readonly JwtOptions _jwtOptions = options.Value ?? throw new ArgumentNullException("Не указанны опции JWT");
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="user">Данные польователя</param>
    /// <param name="token">Токен отмены</param>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserModel user, CancellationToken token)
    {
        UserDto? dto = mapper.Map<UserDto>(user);
        bool ok = await _userService.RegisterAsync(dto,token);
        if (!ok) throw new  BadHttpRequestException("Пользователь уже существует", StatusCodes.Status409Conflict);
        return Ok("Регистрация успешна");
    }

    /// <summary>
    /// Вход
    /// </summary>
    /// <param name="user">Данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserModel user, CancellationToken cancellationToken)
    {
        UserDto? found = await _userService.ValidateAsync(user.Username, user.Password, cancellationToken);
        if (found == null) return Unauthorized();

        Claim[] claims = new[] { new Claim(ClaimTypes.Name, user.Username) };
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}