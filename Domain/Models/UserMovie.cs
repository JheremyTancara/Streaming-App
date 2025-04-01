using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class UserMovie
{
    [Required]
    public int UserMovieID { get; set; } 
    
    [Required]
    public int UserID { get; set; } 
    public List<Movie> Watchlist { get; set; } = new List<Movie>();
    public List<Movie> RecommendedMovies { get; set; } = new List<Movie>();
    public List<Movie> WatchedMovies { get; set; } = new List<Movie>();
}