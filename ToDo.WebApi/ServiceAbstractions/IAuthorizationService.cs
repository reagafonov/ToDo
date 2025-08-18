using ToDo.WebApi.Abstractions;

namespace ToDo.WebApi.ServiceAbstractions;

/// <summary>
/// Авторизация для сущностей
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IAuthorizationService<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Проверяет имеется ли доступ на изменение сущности
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="requestedId">Идентификатор текущего пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    ///<exception cref="UnauthorizedAccessException">Если нет доступа выбрасывается исключение</exception>
    Task CheckCanChangeAsync(Guid id, Guid requestedId, CancellationToken cancellationToken);
}