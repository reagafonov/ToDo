using AutoMapper;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.Users;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Services;

/// <summary>
/// Сервис пользователей
/// </summary>
public class UserService(IRepository<User,UserFilterData> repository, IMapper mapper) : IUserService
{
    /// <summary>
    /// Регистрирует пользователя
    /// </summary>
    /// <param name="userDto">Данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task<bool> RegisterAsync(UserDto userDto, CancellationToken cancellationToken)
    {
        UserFilterData userFilterData = new UserFilterData()
        {
            Username = userDto.Username,
        };

        List<User> filteredAsync = await repository.GetFilteredAsync(userFilterData, cancellationToken);
        if (filteredAsync.Any())
            return false;
        User? user = mapper.Map<User>(userDto);
        repository.Add(user);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Проверка пароля и логина
    /// </summary>
    /// <param name="username">логин</param>
    /// <param name="password">пароль</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task<UserDto?> ValidateAsync(string username, string password, CancellationToken cancellationToken)
    {
        List<User> filteredAsync = await repository.GetFilteredAsync(new UserFilterData()
        {
            Username = username,
        }, cancellationToken);

        if (!filteredAsync.Any() || filteredAsync.Count != 1)
            return null;

        User user = filteredAsync.First();

        return mapper.Map<UserDto>(user);
    }
}