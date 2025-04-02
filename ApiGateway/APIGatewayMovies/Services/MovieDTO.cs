namespace APIGatewayMovies.Services;

    public class MovieDTO
    {


        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string ReleaseDate { get; set; } = string.Empty;

        public string Duration { get; set; } = string.Empty;

        public double Rating { get; set; }


        public string Description { get; set; } = string.Empty;

        public string CastIDs { get; set; } = string.Empty;

        public string DirectorID { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string TrailerUrl { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public int Views { get; set; }

        public int AgeRestriction { get; set; }
    }