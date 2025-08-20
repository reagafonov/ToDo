using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.ServiceAbstractions;

public interface IUserService
{
    /// <summary>
    /// Регистрирует пользователя
    /// </summary>
    /// <param name="userDto">Данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<bool> RegisterAsync(UserDto userDto, CancellationToken cancellationToken);

    /// <summary>
    /// Проверка пароля и логина
    /// </summary>
    /// <param name="username">логин</param>
    /// <param name="password">пароль</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<UserDto?> ValidateAsync(string username, string password, CancellationToken cancellationToken);
}