using Api.Data;
using Api.DTOs;
using Api.Models;
using Api.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Api.Services

{
  public class UserMovieRepository : RepositoryBase<UserMovie, UserMovieDTO>
  {
    private readonly DataContext _context;
    public DataTransformationService genericService;

    public UserMovieRepository(DataContext context)
    {
        _context = context;
        genericService = new DataTransformationService();
    }

    public override async Task<IEnumerable<UserMovie>> GetAllAsync()
    {
      return await _context.Users.ToListAsync();
    }

    public override async Task<UserMovie?> GetByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public override async Task<UserMovie> CreateAsync(UserMovieDTO newUserDTO)
    {
      var newUser = new UserMovie();
      newUser.UserMovieID = await _context.Users.CountAsync() + 1;
      newUser.UserID = newUserDTO.UserID;
      newUser.Watchlist = DataTransformationService.ConvertStringToMovies(newUserDTO.Watchlist);
      newUser.RecommendedMovies = DataTransformationService.ConvertStringToMovies(newUserDTO.RecommendedMovies);
      newUser.WatchedMovies = DataTransformationService.ConvertStringToMovies(newUserDTO.WatchedMovies);

      _context.Users.Add(newUser);
      await _context.SaveChangesAsync();

      return newUser;
    }

    public override async Task Update(int id, UserMovieDTO userDTO)
    {
      var existingUser = await GetByIdAsync(id);

      if (existingUser is not null)
      {
      existingUser.UserID = userDTO.UserID;
      existingUser.Watchlist = DataTransformationService.ConvertStringToMovies(userDTO.Watchlist);
      existingUser.RecommendedMovies = DataTransformationService.ConvertStringToMovies(userDTO.RecommendedMovies);
      existingUser.WatchedMovies = DataTransformationService.ConvertStringToMovies(userDTO.WatchedMovies);
      await _context.SaveChangesAsync();
      }
    }
    }
}
