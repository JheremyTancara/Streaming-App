using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Api.Models;

namespace Api.DTOs

{
    public class RegisterUserDTO
    {
        [JsonIgnore]
        public int UserID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; } // Cambio a DateTime

        [Required]
        public string SubscriptionLevel { get; set; }

        public string ProfilePicture { get; set; } = ProfileSkin.Hulk.ToString();
    }

}