

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace APIGatewayMovies.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            await _next(httpContext);
        }
    }
}
