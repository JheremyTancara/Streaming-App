using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace APIGatewayMovies.Middleware;

public class RateLimitingMiddleware
{

        private readonly RequestDelegate _next;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            await _next(httpContext);
        }

}