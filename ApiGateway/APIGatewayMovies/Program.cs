using APIGatewayMovies.Extensions;
using APIGatewayMovies.Middleware;
using APIGatewayMovies.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


// Registrar servicios
builder.Services.AddServiceServices(); // Servicios personalizados (como RoutingService)
builder.Services.AddHttpClient(); // Registrar IHttpClientFactory
builder.Services.AddControllers(); // Habilitar controladores para la API
builder.Services.AddEndpointsApiExplorer(); // Para explorar los endpoints
builder.Services.AddSwaggerGen(); // Agregar Swagger

var app = builder.Build();

// Configurar el pipeline de middleware
app.UseMiddleware<ErrorHandlingMiddleware>();   // Configurar middleware para manejo de errores
app.UseMiddleware<AuthenticationMiddleware>();  // Configurar middleware para autenticación
app.UseMiddleware<RateLimitingMiddleware>();    // Configurar middleware para limitación de tasa

// Configurar el enrutamiento de controladores
app.MapControllers();

// Configuración de Swagger (documentación de la API)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // UI interactiva de Swagger
}

app.Run();