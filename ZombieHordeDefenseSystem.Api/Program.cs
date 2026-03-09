using Microsoft.EntityFrameworkCore;
using ZombieDefense.Infrastructure.Persistence;
using ZombieHordeDefenseSystem.Application.Ports;
using ZombieHordeDefenseSystem.Application.Services;
using ZombieHordeDefenseSystem.Infraestructure.Repositories;
using Microsoft.OpenApi.Models;
using ZombieHordeDefenseSystem.Api.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("ZombieDefensePolity",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddScoped<IDefenseStrategyService, DefenseStrategyService>();
builder.Services.AddScoped<IZombieRepository, ZombieRepository>();
builder.Services.AddScoped<ISimulationRepository, SimulationRepository>();
builder.Services.AddDbContext<ZombieDefenseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Zombie Horde Defense System API",
        Description = "API para calcular la estrategia de defensa óptima contra una horda de zombies.",
    });

    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Name = "X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Ingresa el API KEY en el campo"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { 
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "ApiKey"
            }
        },
        Array.Empty<string>()
    }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zombie Defense API");
    });


app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("ZombieDefensePolity");
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
