using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.Abstractions.FiltersData;

/// <summary>
/// Интерфейс универсального фильтра для универсального репозитория
/// </summary>
/// <typeparam name="TEntity">Класс сущности</typeparam>
/// <typeparam name="TFilterData">Класс данных фильтра</typeparam>
public interface IFilter<TEntity, in TFilterData> 
   where TEntity: BaseEntity
   where TFilterData: BaseFilterData
{
   /// <summary>
   /// Возвращает запрос с примененным фильтром
   /// </summary>
   /// <param name="filterData">Данные фильтра</param>
   /// <param name="queryable">Исходный запрос</param>
   /// <returns>Запрос с примененным фильтром</returns>
   IQueryable<TEntity> Apply(TFilterData filterData, IQueryable<TEntity> queryable);
}