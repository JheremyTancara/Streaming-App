using Api.Models;

namespace Api.Services
{
    public class RecommendationService
    {
        private readonly List<Movie> _allMovies;

        public RecommendationService(List<Movie> allMovies)
        {
            _allMovies = allMovies;
        }

        public List<Movie> GetTopMoviesByGenre(Genre genre)
        {
            var moviesByGenre = _allMovies
                .Where(m => m.Genre != null && m.Genre.Contains(genre))
                .OrderByDescending(m => m.Views)
                .Take(10)
                .ToList();

            if (!moviesByGenre.Any())
            {
                return GetMostViewedMovies().Take(10).ToList();
            }

            return moviesByGenre;
        }

        public List<Movie> GetMostViewedMovies()
        {
            return _allMovies
                .OrderByDescending(m => m.Views)
                .Take(10)
                .ToList();
        }

        public List<Movie> GetTopRatedMovies()
        {
            return _allMovies
                .OrderByDescending(m => m.Rating)
                .Take(10)
                .ToList();
        }

        public List<Movie> GetRecommendedMovies(UserMovie user, ContentType type)
        {
            if (user.WatchedMovies != null && user.WatchedMovies.Any())
            {
                var firstWatchedMovie = user.WatchedMovies.First();
                if (firstWatchedMovie.Genre != null && firstWatchedMovie.Genre.Any())
                {
                    var genre = firstWatchedMovie.Genre.First();
                    return GetTopMoviesByGenre(genre);
                }
            }

            return GetMostViewedMovies();
        }
    }
}
