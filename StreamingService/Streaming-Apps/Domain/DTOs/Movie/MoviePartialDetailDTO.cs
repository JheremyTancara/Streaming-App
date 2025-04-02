using System.Text.Json.Serialization;
using Api.Validation;

namespace Api.DTOs

{
    public class MoviePartialDetailDTO
    {
        [JsonIgnore]
        public int MovieID { get; set; }

        [Required("ReleaseDate")]
        [StringValue("ReleaseDate")]
        [DateFormat("ReleaseDate")]
        public string ReleaseDate { get; set; } = string.Empty;

        [Required("Description")]
        [StringValue("Description")]
        [LengthRange("Description", 3, 150)]
        public string Description { get; set; } = string.Empty;

        [Required("TrailerUrl")]
        [StringValue("TrailerUrl")]
        [YouTubeUrl("TrailerUrl")]
        [LengthRange("TrailerUrl", 3, 2083)]
        public string TrailerUrl { get; set; } = string.Empty;

        [Required("CastIDs")]
        [StringValue("CastIDs")]
        [CommaSeparatedNumbers("CastIDs")]
        public string[]? CastName { get; set; }

        [Required("DirectorID")]
        [StringValue("DirectorID")]
        [NoSpaces("DirectorID")]
        [NumericString("DirectorID")]
        public string DirectorName { get; set; } = string.Empty;
    }
}
