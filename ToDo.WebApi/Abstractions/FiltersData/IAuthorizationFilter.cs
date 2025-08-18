namespace ToDo.WebApi.Abstractions.FiltersData;

/// <summary>
/// Фильтр авторизации
/// </summary>
/// <typeparam name="TEntity">Сущность</typeparam>
public interface IAuthorizationFilter<TEntity>
{
    /// <summary>
    /// Добавляет фильтр для записей доступных пользователю
    /// </summary>
    /// <param name="queryable">Запрос</param>
    /// <returns>Отфильтрованный запрос или null если доступ к данным запрещен</returns>
    IQueryable<TEntity>? ApplyAuthorization(IQueryable<TEntity> queryable);
}