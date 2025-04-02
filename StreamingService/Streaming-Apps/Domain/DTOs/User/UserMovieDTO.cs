using System.Text.Json.Serialization;

namespace Api.DTOs;

public class UserMovieDTO
{
    [JsonIgnore]
    public int UserMovieId { get; set; } 
    public int UserID { get; set; } 
    public string Watchlist { get; set; } = string.Empty;
    public string RecommendedMovies { get; set; } = string.Empty;
    public string WatchedMovies { get; set; } = string.Empty;
}