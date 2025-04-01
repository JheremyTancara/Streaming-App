using Api.Data;
using Api.DTOs;
using Api.Models;
using Api.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Api.Services

{
  public class ActorRepository : RepositoryBase<Actor, ActorDTO>
  {
    private readonly DataContext _context;

    public ActorRepository(DataContext context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Actor>> GetAllAsync()
    {
      return await _context.Actors.ToListAsync();
    }

    public override async Task<Actor?> GetByIdAsync(int id)
    {
      return await _context.Actors.FindAsync(id);
    }

    public override async Task<Actor> CreateAsync(ActorDTO newActorDTO)
    {
      var newActor = new Actor();
      newActor.ActorID = await _context.Actors.CountAsync() + 1;
      newActor.Name = newActorDTO.Name;
      newActor.Age = newActorDTO.Age;
      newActor.Bio = newActorDTO.Bio;
      newActor.ProfilePictureUrl = newActorDTO.ProfilePictureUrl;

      _context.Actors.Add(newActor);
      await _context.SaveChangesAsync();

      return newActor;
    }

    public override async Task Update(int id, ActorDTO actorDTO)
    {
      var existingActor = await GetByIdAsync(id);

      if (existingActor is not null)
      {
      existingActor.Name = actorDTO.Name;
      existingActor.Age = actorDTO.Age;
      existingActor.Bio = actorDTO.Bio;
      existingActor.ProfilePictureUrl = actorDTO.ProfilePictureUrl;

      await _context.SaveChangesAsync();
    }
    }
 }
}
