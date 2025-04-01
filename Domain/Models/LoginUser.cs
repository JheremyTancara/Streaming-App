namespace Api.Models;

public class LoginUser
{
    public int UserID { get; set; } 
    public string Username { get; set; } = string.Empty;    
    public string Email { get; set; } = string.Empty;
}