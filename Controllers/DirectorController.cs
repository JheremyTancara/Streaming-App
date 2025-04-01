using Api.Models;
using Api.Utilities;
using Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Api.Repositories.Interface;

namespace Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IRepository<Director, DirectorDTO> directorRepository;

        public DirectorController(IRepository<Director, DirectorDTO> _directorRepository)
        {
            directorRepository = _directorRepository;
        }

        [HttpGet(Name = "GetDirectors")]
        public async Task<IEnumerable<Director>> Get()
        {
            return await directorRepository.GetAllAsync();
        }

        [HttpGet("{id}", Name = "GetDirector")]
        public async Task<ActionResult<Director>> GetById(int id)
        {
            var director = await directorRepository.GetByIdAsync(id);

            if (director == null)
            {
                return NotFound(ErrorUtilities.FieldNotFound("Director", id));
            }
            return Ok(director);
        }

        [HttpPost(Name = "AddDirector")]
        public async Task<IActionResult> Create([FromBody] DirectorDTO directorDTO)
        {
            var newDirector = await directorRepository.CreateAsync(directorDTO);
            if (newDirector.Name.Equals("error_409_validations"))
            {
                return Conflict(ErrorUtilities.UniqueName("Director"));
            }

            return CreatedAtAction(nameof(GetById), new { id = newDirector.DirectorID }, directorDTO);
        }

        [HttpPut("{id}", Name = "EditDirector")]
        public async Task<IActionResult> Update(int id, [FromBody] DirectorDTO directorDTO)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorUtilities.IdPositive(id));
            }

            var userToUpdate = await directorRepository.GetByIdAsync(id);

            if (userToUpdate != null)
            {
                await directorRepository.Update(id, directorDTO);
                return NoContent();
            }
            else
            {
                return NotFound(ErrorUtilities.FieldNotFound("Director", id));
            }
        }
    }
}
