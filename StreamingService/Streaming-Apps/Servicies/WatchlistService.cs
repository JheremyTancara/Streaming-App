using Api.Models;

namespace StreamingApp.Services
{
    public class WatchlistService
    {
        public void AddToWatchlist(UserMovie user, Movie movie)
        {
            if (!user.Watchlist.Contains(movie))
            {
                user.Watchlist.Add(movie);
            }
        }
    }
}
