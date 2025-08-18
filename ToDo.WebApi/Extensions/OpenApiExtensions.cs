using System.Reflection;
using Microsoft.OpenApi.Models;

namespace ToDo.WebApi.Extensions;

/// <summary>
/// Конфигурация OpenApiSwagger
/// </summary>
public static class OpenApiExtensions
{
    /// <summary>
    /// Конфигурация сервисов Swagger
    /// </summary>
    /// <param name="services">Билдер контейнера IoC</param>
    /// <returns>Билдер контейнера IoC</returns>
    public static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
       
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "ToDo",
                Description = "Описание Api",
                Contact = new OpenApiContact()
                {
                    Name = "Roman Agafonov",
                    Email = "reagafonov@yandex.ru"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            options.IncludeXmlComments(
                Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);

            // //Настройка безопасности
            // options.AddSecurityRequirement(new OpenApiSecurityRequirement
            // {
            //     {
            //         new OpenApiSecurityScheme
            //         {
            //             Reference = new OpenApiReference
            //             {
            //                 Type = ReferenceType.SecurityScheme,
            //                 Id = "Bearer"
            //             },
            //             Scheme = "bearer",
            //             Name = "Bearer",
            //             In = ParameterLocation.Header,
            //         },
            //         new List<string>()
            //     }
            // });
        });
        return services;
    }

    /// <summary>
    /// Регистрация middleware для swagger
    /// </summary>
    /// <param name="app">Построитель последовательности обработки запроса</param>
    /// <returns>Построитель последовательности обработки запроса</returns>
    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo");
            options.RoutePrefix = "swagger";
        });
        
        return app;
    }
}