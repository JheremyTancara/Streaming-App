using System.Text.Json.Serialization;
using Api.Models;
using Api.Validation;

namespace Api.DTOs

{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class MovieDTO
    {
        [JsonIgnore] public int MovieID { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required] public string Genre { get; set; } = string.Empty;

        [Required] public string ReleaseDate { get; set; } = string.Empty;

        [Required] public string Duration { get; set; } = string.Empty;

        [Required] [Range(0.1, 10.0)] public double Rating { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; } = string.Empty;

        [Required] public string CastIDs { get; set; } = string.Empty;

        [Required] public string DirectorID { get; set; } = string.Empty;

        [Required] [Url] public string ImageUrl { get; set; } = string.Empty;

        [Url] public string TrailerUrl { get; set; } = string.Empty;

        [Required] public string Type { get; set; } = string.Empty;

        [Required] public int Views { get; set; }

        [Required] [Range(1, 25)] public int AgeRestriction { get; set; }
    }
}