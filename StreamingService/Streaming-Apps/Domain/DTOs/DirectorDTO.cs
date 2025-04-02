using System.Text.Json.Serialization;
using Api.Validation;

namespace Api.DTOs

{
    public class DirectorDTO
    {
        [JsonIgnore]
        public int DirectorID { get; set; }

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
        [NoSpecialCharacters("Bio")]
        [LengthRange("Bio", 3, 150)]
        public string Bio { get; set; } = string.Empty;

        [Required("ProfilePictureUrl")]
        [StringValue("ProfilePictureUrl")]
        [ImageUrl("ProfilePictureUrl")]
        [LengthRange("ProfilePictureUrl", 3, 2083)]
        public string ProfilePictureUrl { get; set; } = string.Empty;

        [Required("NumberOfAwards")]
        [IntValue("NumberOfAwards")]
        [RangeInt("NumberOfAwards", 1, 10)]
        public int NumberOfAwards { get; set; }
    }
}
