using Api.Models.Interface;

namespace Api.Models

{
    public class MoviePartialDetail : IMovie
    {
        public int MovieID { get; set; }
        public string ReleaseDate { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public string[]? CastName { get; set; }
        public string DirectorName { get; set; } = string.Empty;
    }
}
