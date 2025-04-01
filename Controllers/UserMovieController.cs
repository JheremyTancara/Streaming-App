using Api.Models;
using Api.Utilities;
using Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Api.Services;

namespace Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMovieController : ControllerBase
    {
        private readonly UserMovieRepository userMovieRepository;

        public UserMovieController(UserMovieRepository _userMovieRepository)
        {
            userMovieRepository = _userMovieRepository;
        }

        [HttpGet(Name = "GetUsersMovie")]
        public async Task<IEnumerable<UserMovie>> Get()
        {
            return await userMovieRepository.GetAllAsync();
        }

        [HttpGet("{id}", Name = "GetUserMovie")]
        public async Task<ActionResult<UserMovie>> GetById(int id)
        {
            var user = await userMovieRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(ErrorUtilities.FieldNotFound("UserMovie", id));
            }
            return Ok(user);
        }
    }
}
