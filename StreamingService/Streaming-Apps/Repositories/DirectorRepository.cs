using Api.Data;
using Api.DTOs;
using Api.Models;
using Api.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Api.Services

{
  public class DirectorRepository : RepositoryBase<Director, DirectorDTO>
  {
    private readonly DataContext _context;

    public DirectorRepository(DataContext context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Director>> GetAllAsync()
    {
      return await _context.Directors.ToListAsync();
    }

    public override async Task<Director?> GetByIdAsync(int id)
    {
      return await _context.Directors.FindAsync(id);
    }

    public override async Task<Director> CreateAsync(DirectorDTO newDirectorDTO)
    {
      var newDirector = new Director();
      newDirector.DirectorID = await _context.Directors.CountAsync() + 1;
      newDirector.Name = newDirectorDTO.Name;
      newDirector.Age = newDirectorDTO.Age;
      newDirector.Bio = newDirectorDTO.Bio;
      newDirector.ProfilePictureUrl = newDirectorDTO.ProfilePictureUrl;
      newDirector.NumberOfAwards = newDirectorDTO.NumberOfAwards;

      _context.Directors.Add(newDirector);
      await _context.SaveChangesAsync();

      return newDirector;
    }

    public override async Task Update(int id, DirectorDTO directorDTO)
    {
      var existingDirector = await GetByIdAsync(id);

      if (existingDirector is not null)
      {
      existingDirector.Name = directorDTO.Name;
      existingDirector.Age = directorDTO.Age;
      existingDirector.Bio = directorDTO.Bio;
      existingDirector.ProfilePictureUrl = directorDTO.ProfilePictureUrl;
      existingDirector.NumberOfAwards = directorDTO.NumberOfAwards;

      await _context.SaveChangesAsync();
      }
    }
  }
}
