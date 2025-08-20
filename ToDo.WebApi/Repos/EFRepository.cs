using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Abstractions.FiltersData;
using ToDo.WebApi.Domain;

namespace ToDo.WebApi.Repos;

/// <summary>
/// Обобщенный репозиторий для EntityFramework
/// </summary>
/// <param name="context">Контекст хранилища</param>
/// <param name="filter">Класс, применяющий фильтрацию и пагинацию</param>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TFilterData">Тип данных фильтрации</typeparam>
public class EfRepository<TEntity, TFilterData>(DataContext context, IFilter<TEntity, TFilterData> filter, IMapper mapper)
    : IRepository<TEntity, TFilterData>
    where TEntity : BaseEntity
    where TFilterData : BaseFilterData
{
    /// <summary>
    /// Получение отфильтрованного списка сущностей
    /// </summary>
    /// <param name="filterData">Данные фильтра</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task<List<TEntity>> GetFilteredAsync(TFilterData filterData,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = context.Set<TEntity>().AsQueryable();
        query = filter.Apply(filterData, query);
        return await query.AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Получение списка всех тасок
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);

    /// <summary>
    /// Получает задачу по id
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Найденная задачи или null</returns>
    public async Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken) =>
        await context.Set<TEntity>().AsNoTracking().Where(entity => entity.Id == id).FirstOrDefaultAsync(cancellationToken);

    /// <summary>
    /// Добавляет задачу в хранилище
    /// </summary>
    /// <param name="entity">Сущность задачи</param>
    /// <returns>Идентификатор добавленной записи</returns>
    public void Add(TEntity entity)
    {
        context.Add(entity);
       
    }

    /// <summary>
    /// Обновляет задачу в хранилище
    /// </summary>
    /// <param name="entity">Новые данные со старым идентификатором</param>
    public void Update(TEntity entity)
    {
        context.Update(entity);
        // var oldValues = await context.Set<TEntity>().FindAsync(entity.Id, cancellationToken);
        // if (oldValues == null)
        //     throw new KeyNotFoundException();
        // mapper.Map(entity, oldValues);
        // context.Update(entity);
    }

    /// <summary>
    /// Удаляет задачу из хранилища
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <remarks>Бросает исключение KryNotFoundException если сущность не найдена</remarks>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity? entityEntry = await context.FindAsync<TEntity>(id, cancellationToken);

        if (entityEntry is null)
            throw new KeyNotFoundException();

        entityEntry.IsDeleted = true;
        context.Update(entityEntry);
    }

    /// <summary>
    /// Восстановление задачи
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task RestoreAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity? entityEntry = await context.Set<TEntity>().IgnoreQueryFilters()
            .Where(baseEntity => baseEntity.IsDeleted && baseEntity.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        //entityEntry != null && entityEntry.IsDeleted 
        if (entityEntry is { IsDeleted: true })
        {
            entityEntry.IsDeleted = false;
            context.Update(entityEntry);
        }
    }

    /// <summary>
    /// Удаляет набор задач
    /// </summary>
    /// <param name="elements">Сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task DeleteAsync(IEnumerable<TEntity> elements, CancellationToken cancellationToken)
    {
        // List<TEntity> elements = await context.Set<TEntity>().Where(entity => ids.Contains(entity.Id))
        //     .ToListAsync(cancellationToken);

        foreach (TEntity element in elements)
        {
            element.IsDeleted = true;
            context.Update(element);
        }

        // await context.SaveChangesAsync(cancellationToken);
        //
        // return elements.Select(e => e.Id).ToList();
    }

    /// <summary>
    /// Осуществляет запись измененных данных в базу
    /// </summary>
    /// <param name="cancellation">Токен отмены</param>
    public async Task SaveChangesAsync(CancellationToken cancellation = default)
    {
        await context.SaveChangesAsync(cancellation);
        context.ChangeTracker.Clear();
    }
}