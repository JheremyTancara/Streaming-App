using System.Text;
using System.Text.Json;
using APIGatewayMovies.Configurations;

namespace APIGatewayMovies.Services.RoutingStreaming;

public class RoutingActors
{
       private readonly IHttpClientFactory _httpClientFactory;

        public RoutingActors(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<object> GetActorsFromService()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(ServiceEndpoints.ActorServiceUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var actors = JsonSerializer.Deserialize<List<object>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return actors;
            }

            return new { error = "Error al obtener los actores." };
        }

        public async Task<object> CreateActor(ActorDto newActor)
        {
            var client = _httpClientFactory.CreateClient(); 

            var jsonContent = JsonSerializer.Serialize(newActor); 
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var response = await client.PostAsync(ServiceEndpoints.ActorServiceUrl, content);

        
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync(); 
               
                return JsonSerializer.Deserialize<object>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new { error = "Error al crear el actor." };
        }


        public async Task<object> GetActorById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{ServiceEndpoints.ActorServiceUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var actor = JsonSerializer.Deserialize<object>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return actor;
            }

            return new { error = "Error al obtener el actor." };
        }

}