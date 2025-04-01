using System.Text.Json.Serialization;
using Api.Models;
using Api.Validation;

namespace Api.DTOs

{
    public class MovieDTO
    {
        [JsonIgnore]
        public int MovieID { get; set; }

        [Required("Title")]
        [StringValue("Title")]
        [NoSpecialCharacters("Title")]
        [LengthRange("Title", 3, 150)]
        public string Title { get; set; } = string.Empty;    

        [Required("Genre")]
        [StringValue("Genre")]
        [WithinListEnumValues(typeof(Genre), "Genre")]
        public string Genre { get; set; } = string.Empty;

        [Required("Genre")]
        [StringValue("Genre")]
        [NoSpaces("Duration")]
        [DateFormat("ReleaseDate")]
        public string ReleaseDate { get; set; } = string.Empty;

        [Required("Duration")]
        [StringValue("Duration")]
        [NoSpaces("Duration")]
        [TimeFormat("Duration")]
        public string Duration { get; set; } = string.Empty;

        [Required("Rating")]
        [DoubleValue("Rating")] 
        [RangeDouble("Rating", 0.1, 10.0)]
        public double Rating { get; set; }

        [Required("Description")]
        [StringValue("Description")]
        [LengthRange("Description", 3, 150)]
        public string Description { get; set; } = string.Empty;

        [Required("CastIDs")]
        [StringValue("CastIDs")]
        [CommaSeparatedNumbers("CastIDs")]
        public string CastIDs { get; set; } = string.Empty;

        [Required("DirectorID")]
        [StringValue("DirectorID")]
        [NoSpaces("DirectorID")]
        [NumericString("DirectorID")]
        public string DirectorID { get; set; } = string.Empty;

        [Required("ImageUrl")]
        [StringValue("ImageUrl")]
        [ImageUrl("ImageUrl")]
        [LengthRange("ImageUrl", 3, 2083)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required("TrailerUrl")]
        [StringValue("TrailerUrl")]
        [YouTubeUrl("TrailerUrl")]
        [LengthRange("TrailerUrl", 3, 2083)]
        public string TrailerUrl { get; set; } = string.Empty;

        [Required("Type")]
        [StringValue("Type")]
        [NoSpecialCharacters("Type")]
        [WithinEnumValues(typeof(ContentType), "Type")]
        public string Type { get; set; } = string.Empty;

        [Required("Views")]
        [IntValue("Views")]
        public int Views { get; set; }

        [Required("Views")]
        [IntValue("Views")]
        [RangeInt("Age", 1, 25)]
        public int AgeRestriction { get; set; }
    }
}
