using System.Text;
using Api.Data;
using Api.DTOs;
using Api.Models;
using Api.Models.Interface;
using Api.Repositories.Interface;
using Api.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno
Env.Load();

builder.Configuration["Jwt:Key"] = Environment.GetEnvironmentVariable("JWT_KEY")
    ?? throw new ArgumentNullException("JWT_KEY", "JWT_KEY is missing in environment variables");

builder.Configuration["Jwt:Issuer"] = Environment.GetEnvironmentVariable("JWT_ISSUER")
    ?? throw new ArgumentNullException("JWT_ISSUER", "JWT_ISSUER is missing in environment variables");

builder.Configuration["Jwt:Audience"] = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
    ?? throw new ArgumentNullException("JWT_AUDIENCE", "JWT_AUDIENCE is missing in environment variables");

builder.Configuration["Jwt:Subject"] = Environment.GetEnvironmentVariable("JWT_SUBJECT")
    ?? throw new ArgumentNullException("JWT_SUBJECT", "JWT_SUBJECT is missing in environment variables");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Streaming Project", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuración de Autenticación JWT
var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Configuración de CORS
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? new string[] {};

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins(allowedOrigins)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new LowercaseControllerModelConvention());
});

// Configuración de MySQL
var connectionString = builder.Configuration.GetConnectionString("MySQLConnection")
    ?? throw new ArgumentNullException("MySQLConnection", "Database connection string is missing.");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 25)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    ));

// Registro de servicios
builder.Services.AddScoped<IRepository<IMovie, MovieDTO>, MovieRepository>();
builder.Services.AddScoped<IRepository<Actor, ActorDTO>, ActorRepository>();
builder.Services.AddScoped<IRepository<Director, DirectorDTO>, DirectorRepository>();

var app = builder.Build();

// Migraciones y Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

    if (context.Database.CanConnect())
    {
        await context.SeedData();
    }
    else
    {
        Console.WriteLine("⚠️ Warning: No database connection. Skipping SeedData.");
    }
}

// Configuración de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigins");
app.MapControllers();
app.Run();

public class LowercaseControllerModelConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        controller.ControllerName = controller.ControllerName.ToLower();

        foreach (var selectorModel in controller.Selectors)
        {
            var attributeRouteModel = selectorModel.AttributeRouteModel;

            if (attributeRouteModel?.Template != null)
            {
                attributeRouteModel.Template = attributeRouteModel.Template.ToLower();
            }
        }
    }
}