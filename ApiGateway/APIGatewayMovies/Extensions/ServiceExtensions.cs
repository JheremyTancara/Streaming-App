using APIGatewayMovies.Services;
using APIGatewayMovies.Services.RoutingStreaming;
using APIGatewayMovies.Services.RoutingUser;

namespace APIGatewayMovies.Extensions;


    public static class ServiceExtensions
    {
        public static void AddServiceServices(this IServiceCollection services)
        {
            services.AddScoped<RoutingActors>();
            services.AddScoped<RoutingUser>();
            services.AddScoped<RoutingDirector>();
            services.AddScoped<RoutingMovies>();

        }

}