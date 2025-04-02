using Api.Data;
using Api.DTOs;
using Api.Models;
using Api.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Api.Services

{
  public class UserRepository : RepositoryBase<User, RegisterUserDTO>
  {
    private readonly DataContext _context;
    public DataTransformationService genericService;

    public UserRepository(DataContext context)
    {
        _context = context;
        genericService = new DataTransformationService();
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
      return await _context.Users.ToListAsync();
    }

    public override async Task<User?> GetByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public override async Task<User> CreateAsync(RegisterUserDTO newUserDTO)
    {
      if (!await genericService.IsBrandNameUnique(_context, newUserDTO.Username))
      {
        var newUser = new User
        {
          UserID = await _context.Users.CountAsync() + 1,
          Username = newUserDTO.Username,
          Email = newUserDTO.Email,
          Password = newUserDTO.Password,
          DateOfBirth = newUserDTO.DateOfBirth,
          SubscriptionLevel = DataTransformationService.ConvertToSubscriptionLevel(newUserDTO.SubscriptionLevel),
          ProfilePicture = DataTransformationService.ConvertToProfileSkin(newUserDTO.ProfilePicture)
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
      }

      throw new InvalidOperationException("Username already exists.");
    }


    public override async Task Update(int id, RegisterUserDTO userDTO)
    {
      var existingUser = await GetByIdAsync(id);

      if (existingUser is not null)
      {
      existingUser.Username = userDTO.Username;
      existingUser.Email = userDTO.Email;
      existingUser.Password = userDTO.Password;
      existingUser.DateOfBirth = userDTO.DateOfBirth;
      existingUser.SubscriptionLevel = DataTransformationService.ConvertToSubscriptionLevel(userDTO.SubscriptionLevel);
      existingUser.ProfilePicture = DataTransformationService.ConvertToProfileSkin(userDTO.ProfilePicture);
      await _context.SaveChangesAsync();
      }
    }
  }
}
