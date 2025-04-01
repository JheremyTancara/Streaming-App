using Api.Models;
using Api.Utilities;
using Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Api.Repositories.Interface;

namespace Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IRepository<Actor, ActorDTO> actorRepository;

        public ActorController(IRepository<Actor, ActorDTO> _actorRepository)
        {
            actorRepository = _actorRepository;
        }

        [HttpGet(Name = "GetActors")]
        public async Task<IEnumerable<Actor>> Get()
        {
            return await actorRepository.GetAllAsync();
        }

        [HttpGet("{id}", Name = "GetActor")]
        public async Task<ActionResult<Actor>> GetById(int id)
        {
            var actor = await actorRepository.GetByIdAsync(id);

            if (actor == null)
            {
                return NotFound(ErrorUtilities.FieldNotFound("Actor", id));
            }
            return Ok(actor);
        }

        [HttpPost(Name = "AddActor")]
        public async Task<IActionResult> Create([FromBody] ActorDTO actorDTO)
        {
            var newActor = await actorRepository.CreateAsync(actorDTO);
            if (newActor.Name.Equals("error_409_validations"))
            {
                return Conflict(ErrorUtilities.UniqueName("Actor"));
            }

            return CreatedAtAction(nameof(GetById), new { id = newActor.ActorID }, actorDTO);
        }

        [HttpPut("{id}", Name = "EditActor")]
        public async Task<IActionResult> Update(int id, [FromBody] ActorDTO actorDTO)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorUtilities.IdPositive(id));
            }

            var actorToUpdate = await actorRepository.GetByIdAsync(id);

            if (actorToUpdate != null)
            {
                await actorRepository.Update(id, actorDTO);
                return NoContent();
            }
            else
            {
                return NotFound(ErrorUtilities.FieldNotFound("Actor", id));
            }
        }
    }
}
