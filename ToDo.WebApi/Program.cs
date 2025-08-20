using ToDo.WebApi.Extensions;
using ToDo.WebApi.Jwt.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJwtConfigurationSources();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

IConfiguration configuration = builder.Configuration;

builder.Services
    .AddAutoMapper(config => { }, typeof(Program).Assembly)
    .RegisterRepositories()
    .RegisterServices()
    .RegisterEntityFramework(configuration)
    //.RegisterKeycloakAuthentication(configuration)
    .RegisterSwagger()
    .RegisterControllers()
    .RegiserAuthorization()
    .RegisterMongoServices(configuration)
    .RegisterJwtAuthentication(configuration);

builder.Host.RegisterSerilogConsole();

WebApplication app = builder.Build();

app.AutoApplyMigrations();

app.UseJwtAuthentication();
//app.UseKeycloakAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

app.UseHttpsRedirection();

app.RegisterControllers();

app.Run();

