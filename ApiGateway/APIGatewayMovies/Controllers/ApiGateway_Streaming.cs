

using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using APIGatewayMovies.Services;
using APIGatewayMovies.Services.RoutingStreaming;

namespace APIGatewayMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGateway_Streaming : ControllerBase
    {
        private readonly RoutingActors _routingService;
        private readonly RoutingDirector _routingDirector;
        private readonly RoutingMovies _routingMovies;

        public ApiGateway_Streaming(RoutingActors routingService, RoutingDirector routingDirector, RoutingMovies routingMovies)
        {
            _routingService = routingService;
            _routingDirector = routingDirector;
            _routingMovies = routingMovies;
        }
        [HttpGet("actors")]
        public async Task<IActionResult> GetActors()
        {
            var result = await _routingService.GetActorsFromService();
            return Ok(result);
        }
        [HttpGet("actors/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _routingService.GetActorById(id);
            return Ok(result);
        }

        [HttpPost("actors")]
        public async Task<IActionResult> CreateUser([FromBody] ActorDto newActor)
        {
            if (newActor == null)
            {
                return BadRequest(new { error = "El cuerpo de la solicitud no puede estar vacío." });
            }

            var result = await _routingService.CreateActor(newActor);
            return Ok(result);
        }

        [HttpGet("movies")]
        public async Task<IActionResult> GetMovie()
        {
            var result = await _routingMovies.GetMoviesFromService();
            return Ok(result);
        }
        [HttpGet("movies/{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var result = await _routingMovies.GetMovieById(id);
            return Ok(result);
        }

        [HttpPost("movies")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieDTO newActor)
        {
            if (newActor == null)
            {
                return BadRequest(new { error = "El cuerpo de la solicitud no puede estar vacío." });
            }

            var result = await _routingMovies.CreateMovie(newActor);
            return Ok(result);
        }

        [HttpGet("director")]
        public async Task<IActionResult> GetDirector()
        {
            var result = await _routingDirector.GetDirectorFromService();
            return Ok(result);
        }
        [HttpGet("director/{id}")]
        public async Task<IActionResult> GetDirectorrById(int id)
        {
            var result = await _routingDirector.GetDirectorById(id);
            return Ok(result);
        }

        [HttpPost("director")]
        public async Task<IActionResult> CreateDirector([FromBody] DirectorDTO newActor)
        {
            if (newActor == null)
            {
                return BadRequest(new { error = "El cuerpo de la solicitud no puede estar vacío." });
            }

            var result = await _routingDirector.CreateDirector(newActor);
            return Ok(result);
        }



    }
}
