using System.Text;
using System.Text.Json;
using APIGatewayMovies.Configurations;

namespace APIGatewayMovies.Services.RoutingStreaming;

public class RoutingDirector
{
    private readonly IHttpClientFactory _httpClientFactory;

        public RoutingDirector(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<object> GetDirectorFromService()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(ServiceEndpoints.DirectorServiceUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var director = JsonSerializer.Deserialize<List<object>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return director;
            }

            return new { error = "Error al obtener los directores." };
        }

        public async Task<object> CreateDirector(DirectorDTO newDirector)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonContent = JsonSerializer.Serialize(newDirector); 
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var response = await client.PostAsync(ServiceEndpoints.DirectorServiceUrl, content);


            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<object>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new { error = "Error al crear el director." };
        }


        public async Task<object> GetDirectorById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{ServiceEndpoints.DirectorServiceUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var director = JsonSerializer.Deserialize<object>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return director;
            }

            return new { error = "Error al obtener el actor." };
        }

}