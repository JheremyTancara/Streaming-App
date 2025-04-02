namespace APIGatewayMovies.Services;

public class UserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string DateOfBirth { get; set; }
    public string SubscriptionLevel { get; set; }
    public string ProfilePicture { get; set; }
}