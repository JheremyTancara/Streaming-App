using APIGatewayMovies.Middleware;

namespace APIGatewayMovies.Extensions;

public static class MiddlewareExtensions
{
    public static void AddMiddlewareServices(this IServiceCollection services)
    {
        services.AddTransient<ErrorHandlingMiddleware>();
        services.AddTransient<AuthenticationMiddleware>();
        services.AddTransient<RateLimitingMiddleware>();
    }
}