namespace Api.Models;

public class User
{
    public int UserID { get; set; } 
    public string Username { get; set; } = string.Empty;    
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } 
    public SubscriptionLevel SubscriptionLevel { get; set; }
    public ProfileSkin ProfilePicture { get; set; }
}
