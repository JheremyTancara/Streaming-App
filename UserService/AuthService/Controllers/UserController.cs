using Api.Models;
using Api.Utilities;
using Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Api.Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly DataTransformationService genericService;

        public UserController(UserRepository _userRepository, DataTransformationService _genericService)
        {
            userRepository = _userRepository;
            genericService = _genericService;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IEnumerable<User>> Get()
        {
            return await userRepository.GetAllAsync();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(ErrorUtilities.FieldNotFound("User", id));
            }
            return Ok(user);
        }

        [HttpPost(Name = "AddUser")]
        public async Task<IActionResult> Create([FromBody] RegisterUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }



            var newUser = await userRepository.CreateAsync(userDTO);

            return CreatedAtAction(nameof(GetById), new { id = newUser.UserID }, userDTO);
        }

        [HttpPut("{id}", Name = "EditUser")]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterUserDTO userDTO)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorUtilities.IdPositive(id));
            }

            var userToUpdate = await userRepository.GetByIdAsync(id);

            if (userToUpdate != null)
            {
                await userRepository.Update(id, userDTO);
                return NoContent();
            }
            else
            {
                return NotFound(ErrorUtilities.FieldNotFound("User", id));
            }
        }
    }
}
