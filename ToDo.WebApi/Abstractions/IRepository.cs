namespace ToDo.WebApi.Abstractions;

public interface IRepository<TEntity,in TFilterData> where TEntity : BaseEntity
{

    /// <summary>
    /// Получение отфильтрованного списка сущностей
    /// </summary>
    /// <param name="filterData">Данные фильтра</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<List<TEntity>> GetFilteredAsync(TFilterData filterData,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Возвращает все задачи
    /// </summary>
    /// <returns>Все задачи</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellation = default);

    /// <summary>
    /// Получает задачу по id
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellation">Токен отмены</param>
    /// <returns>Найденная задачи или null</returns>
    Task<TEntity?> GetAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Добавляет задачу в хранилище
    /// </summary>
    /// <param name="entity">Сущность задачи</param>
    /// <param name="cancellation">Токен отмены</param>
    Task AddAsync(TEntity entity, CancellationToken cancellation = default);

    /// <summary>
    /// Обновляет задачу в хранилище
    /// </summary>
    /// <param name="entity">Новые данные со старым идентификатором</param>
    /// <param name="cancellation">Токен отмены</param>
    Task UpdateAsync(TEntity entity, CancellationToken cancellation = default);

    /// <summary>
    /// Удаляет задачу из хранилища
    /// </summary>
    /// <param name="id">Идентификатор сущности/param>
    /// <param name="cancellation">Токен отмены</param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Восстановление задачи
    /// </summary>
    /// <param name="id">ID сущности</param>
    /// <param name="cancellation">Токен отмены</param>
    /// <returns></returns>
    Task RestoreAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Удаляет набор задач
    /// </summary>
    /// <param name="entities">Сущности</param>
    /// <param name="cancellation">Токен отмены</param>
    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellation = default);
    
    /// <summary>
    /// Осуществляет запись измененных данных в базу
    /// </summary>
    /// <param name="cancellation"></param>
    Task SaveChangesAsync(CancellationToken cancellation = default);
}