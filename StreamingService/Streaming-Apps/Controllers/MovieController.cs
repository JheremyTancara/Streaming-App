using Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Api.DTOs;
using Api.Repositories.Interface;
using Api.Models.Interface;
using Api.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Api.Data;

namespace Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IRepository<IMovie, MovieDTO> movieRepository;
        private readonly DataContext _context; 

        public MovieController(IRepository<IMovie, MovieDTO> _movieRepository, DataContext context)
        {
            movieRepository = _movieRepository;
            _context = context;
        }

        [HttpGet("home-page", Name = "GetMovies")]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await movieRepository.GetAllAsync(); 
            return Ok(movies);
        }


        [HttpGet("partial-detail/{id}", Name = "GetMovie")]
        [Authorize]
        public async Task<ActionResult<MoviePartialDetail>> GetByIdPartialDetail(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity), "The identity value cannot be null.");
            }

            var rToken = Jwt.validarToken(identity, _context);
            if (!rToken.success) return Unauthorized(rToken);

            User usuario = rToken.result;

            var movieDetail = await movieRepository.GetByIdAsync(id);

            if (movieDetail == null)
            {
                return NotFound(ErrorUtilities.FieldNotFound("Movie", id));
            }
            var partialDetail = (MoviePartialDetail)movieDetail;
            return Ok(partialDetail);
        }


        [HttpPost(Name = "AddMovie")]
        public async Task<IActionResult> Create([FromBody] MovieDTO movieDTO)
        {
            var newMovie = await movieRepository.CreateAsync(movieDTO);
            var movie = newMovie as Movie;
            
            if (movie == null)
            {
                return BadRequest("Error creating the movie."); 
            }

            return CreatedAtAction(nameof(GetByIdPartialDetail), new { id = newMovie.MovieID }, movieDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }



        [HttpPut("{id}", Name = "EditMovie")]
        public async Task<IActionResult> Update(int id, [FromBody] MovieDTO MovieDTO)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorUtilities.IdPositive(id));
            }

            var userToUpdate = await movieRepository.GetByIdAsync(id);

            if (userToUpdate != null)
            {
                await movieRepository.Update(id, MovieDTO);
                return NoContent();
            }
            else
            {
                return NotFound(ErrorUtilities.FieldNotFound("Movie", id));
            }
        }
    }
}
