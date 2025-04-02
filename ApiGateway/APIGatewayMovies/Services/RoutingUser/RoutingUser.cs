using System.Text;
using System.Text.Json;
using APIGatewayMovies.Configurations;

namespace APIGatewayMovies.Services.RoutingUser;

public class RoutingUser
{
        private readonly IHttpClientFactory _httpClientFactory;
        public RoutingUser(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<object> GetUsersFromService()
        {
            var client = _httpClientFactory.CreateClient("UserService");

            var response = await client.GetAsync(ServiceEndpoints.UserServiceUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<object>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new { error = "Error al obtener los usuarios." };
        }

        public async Task<object> CreateUser(UserDto newUser)
        {
            var client = _httpClientFactory.CreateClient("UserService");

            var jsonContent = JsonSerializer.Serialize(newUser);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ServiceEndpoints.UserServiceUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<object>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new { error = "Error al crear el usuario." };
        }

        public async Task<object> GetUserById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{ServiceEndpoints.UserServiceUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<object>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user;
            }

            return new { error = "Error al obtener el usuario." };
        }

        public async Task<bool> UpdateUser(int id, object userData)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonContent = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{ServiceEndpoints.UserServiceUrl}/{id}", jsonContent);

            return response.IsSuccessStatusCode;
        }

}