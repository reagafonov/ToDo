using ToDo.WebApi.Abstractions;
using ToDo.WebApi.Abstractions.FiltersData;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.MongoService;
using ToDo.WebApi.Repos;
using ToDo.WebApi.Repos.CommonFilters;
using ToDo.WebApi.Repos.FiltersData;
using ToDo.WebApi.Repos.UserTaskLists;
using ToDo.WebApi.Repos.UserTasks;
using ToDo.WebApi.ServiceAbstractions;
using ToDo.WebApi.Services;

namespace ToDo.WebApi.Extensions;

/// <summary>
/// Расширения IoC
/// </summary>
public static class DiExtensions
{
    /// <summary>
    /// Подключение репозиториев
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
       
        //Обобщенные репозитории
        services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));

        services.AddScoped<IFilter<UserTask,UserTaskFilterData>,UserTaskFilter>();
        services.AddScoped<IFilter<UserTaskList, UserTaskListFilterData>, UserTaskListFilter>();
        return services;
    }

    /// <summary>
    /// Регистрирует фильтры для авторизации сущностей
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterAuthorizationFilters(this IServiceCollection services)
    {
        /*цепочка подфильтров базового филтьра*/
        //Обобщенные адаптеры авторизации для фильтра авторизации по умолчанию
        services.AddScoped(typeof(ICommonFilter<,>), typeof(AuthorizationCommonFilterAdapter<,>));
        
        //Подфильтры авторизации по умолчанию для сущностей без кастомного фильтра
        services.AddScoped(typeof(IAuthorizationFilter<>), typeof(CommonAuthorizationFilter<>));
       
        /*Кастомные фильтры авторизации для сущностей можно зарегистрировать здесь*/
        
        return services;
    }
    
    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        //Сервис задач
        services.AddScoped<IUserTaskService, UserTaskService>();
        
        //Сервис списков
        services.AddScoped<IUserTaskListService, UserTaskListService>();

        //Сервер пользователей
        services.AddSingleton<IUserService, DefaultUserService>();
        
        //сервер документов
        services.AddScoped<IDocumentService, MongoSmallDocumentService>();
        
        return services;
    }
    
   
}