using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.Repos.CommonFilters;

/// <summary>
/// Интерфейс для цепочки базовых фильтров
/// </summary>
/// <typeparam name="TEntity">Сущность</typeparam>
/// <typeparam name="TFilterData">Данные фильтра</typeparam>
public interface ICommonFilter<TEntity, in TFilterData>
    where TEntity:BaseEntity
{
    /// <summary>
    /// Шаблон фильтра
    /// </summary>
    /// <param name="filterData">Данные фильтра</param>
    /// <param name="queryable">Фильруемый запрос</param>
    /// <returns>Отфильтрованный запрос</returns>
    IQueryable<TEntity>? ApplyPart(TFilterData filterData, IQueryable<TEntity> queryable);
}

