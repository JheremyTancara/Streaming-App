using Api.Models.Interface;

namespace Api.Models

{
    public class MovieHomePage : IMovie
    {
        public int MovieID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Views { get; set; }
        public int AgeRestriction { get; set; }
    }
}
