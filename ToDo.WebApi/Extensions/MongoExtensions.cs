using MongoDB.Driver;
using ToDo.WebApi.Domain;

namespace ToDo.WebApi.Extensions;

/// <summary>
/// Подключение MongoDb
/// </summary>
public static class MongoExtensions
{
    /// <summary>
    /// Регистрация сервисов Mongo
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <param name="configuration">Конфигурация</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection RegisterMongoServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoClient>(s=> new MongoClient(configuration.GetValue<string>("ConnectionStrings:MongoDb")));
        
        services.AddScoped<IMongoDatabase>(s=> s.GetRequiredService<IMongoClient>().GetDatabase(configuration.GetValue<string>("Mongo:DbName")));

        services.AddScoped(s => new MongoContext(s.GetRequiredService<IMongoDatabase>()));
        
        return services;
    }
}