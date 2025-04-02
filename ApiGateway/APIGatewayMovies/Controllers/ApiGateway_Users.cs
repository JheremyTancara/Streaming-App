
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using APIGatewayMovies.Services;
using APIGatewayMovies.Services.RoutingUser;

namespace APIGatewayMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGateway_Users : ControllerBase
    {
        private readonly RoutingUser _routingService;

        public ApiGateway_Users(RoutingUser routingService)
        {
            _routingService = routingService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _routingService.GetUsersFromService();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto newUser)
        {
            if (newUser == null)
            {
                return BadRequest(new { error = "El cuerpo de la solicitud no puede estar vacío." });
            }

            var result = await _routingService.CreateUser(newUser);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _routingService.GetUserById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] object userData)
        {
            var result = await _routingService.UpdateUser(id, userData);
            return Ok(result);
        }

    }
}
