using Api.Models;
using NUnit.Framework;
using StreamingApp.Services;

namespace StreamingApp.Tests
{
    [TestFixture]
    public class WatchlistServiceTests
    {
        private WatchlistService _watchlistService;
        private UserMovie _user;
        private Movie _movie;

        [SetUp]
        public void Setup()
        {
            _watchlistService = new WatchlistService();
            _user = new UserMovie { UserMovieID = 1, Watchlist = new List<Movie>() };
            _movie = new Movie { MovieID = 1, Title = "Movie 1" };
        }

        [Test]
        public void AddToWatchlist_AddsMovieToWatchlist()
        {
            _watchlistService.AddToWatchlist(_user, _movie);

            Assert.Contains(_movie, _user.Watchlist);
        }

        [Test]
        public void AddToWatchlist_DoesNotAddDuplicateMovies()
        {
            _watchlistService.AddToWatchlist(_user, _movie);
            _watchlistService.AddToWatchlist(_user, _movie); 

            Assert.AreEqual(1, _user.Watchlist.Count);
        }
    }
}
