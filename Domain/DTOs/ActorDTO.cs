using System.Text.Json.Serialization;
using Api.Validation;

namespace Api.DTOs

{
    public class ActorDTO
    {
        [JsonIgnore]
        public int ActorID { get; set; }

        [Required("Name")]
        [StringValue("Name")]
        [NoSpecialCharacters("Name")]
        [NoNumbers("Name")]
        [LengthRange("Name", 3, 40)]
        public string Name { get; set; } = string.Empty;

        [Required("Age")]
        [IntValue("Age")]
        [RangeInt("Age", 1, 125)]
        public int Age { get; set; }

        [Required("Bio")]
        [StringValue("Bio")]
        [LengthRange("Bio", 3, 150)]
        public string Bio { get; set; } = string.Empty;

        [Required("ProfilePictureUrl")]
        [StringValue("ProfilePictureUrl")]
        [ImageUrl("ProfilePictureUrl")]
        [LengthRange("ProfilePictureUrl", 3, 2083)]
        public string ProfilePictureUrl { get; set; } = string.Empty;
    }
}
