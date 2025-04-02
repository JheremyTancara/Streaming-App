using System.Text;
using System.Text.Json;
using APIGatewayMovies.Configurations;

namespace APIGatewayMovies.Services.RoutingStreaming;

public class RoutingMovies
{
    private readonly IHttpClientFactory _httpClientFactory;

        public RoutingMovies(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<object> GetMoviesFromService()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{ServiceEndpoints.MovieServiceUrl}/home-page");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var movies = JsonSerializer.Deserialize<List<object>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return movies;
            }

            return new { error = "Error al obtener los actores." };
        }

        public async Task<object> CreateMovie(MovieDTO newActor)
        {
            var client = _httpClientFactory.CreateClient(); 

            var jsonContent = JsonSerializer.Serialize(newActor); 
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"); 

            var response = await client.PostAsync(ServiceEndpoints.MovieServiceUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync(); 
                return JsonSerializer.Deserialize<object>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new { error = "Error al crear la pelicula." };
        }


        public async Task<object> GetMovieById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{ServiceEndpoints.MovieServiceUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var actor = JsonSerializer.Deserialize<object>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return actor;
            }

            return new { error = "Error al obtener la pelicula." };
        }

}