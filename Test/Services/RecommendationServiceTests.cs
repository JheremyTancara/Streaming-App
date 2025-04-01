using NUnit.Framework;
using Api.Models;
using Api.Services;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Tests.Services
{
    [TestFixture]
    public class RecommendationServiceTests
    {
        private RecommendationService? _recommendationService; 
        private List<Movie>? _allMovies;

        [SetUp]
        public void Setup()
        {
            _allMovies = new List<Movie>
            {
                new Movie { MovieID = 1, Title = "Movie 1", Genre = new List<Genre> { Genre.Action }, Views = 1000, Rating = 4.5, Type = ContentType.Movie },
                new Movie { MovieID = 2, Title = "Movie 2", Genre = new List<Genre> { Genre.Comedy }, Views = 2000, Rating = 4.0, Type = ContentType.Movie },
                new Movie { MovieID = 3, Title = "Movie 3", Genre = new List<Genre> { Genre.Action }, Views = 3000, Rating = 5.0, Type = ContentType.Series },
                new Movie { MovieID = 4, Title = "Movie 4", Genre = new List<Genre> { Genre.Drama }, Views = 1500, Rating = 4.8, Type = ContentType.Movie },
                new Movie { MovieID = 5, Title = "Movie 5", Genre = new List<Genre> { Genre.Action }, Views = 2500, Rating = 4.6, Type = ContentType.Movie },
            };

            _recommendationService = new RecommendationService(_allMovies);
        }

        [Test]
        public void GetRecommendedMovies_ByGenre_ReturnsMostViewed()
        {
            var user = new UserMovie
            {
                WatchedMovies = new List<Movie> { new Movie { Genre = new List<Genre> { Genre.Action } } },
                Watchlist = new List<Movie>()
            };

            var recommendedMovies = _recommendationService!.GetRecommendedMovies(user, ContentType.Movie);

            Assert.IsTrue(recommendedMovies.All(m => m.Genre?.Contains(Genre.Action) == true)); 
            Assert.That(recommendedMovies, Is.Ordered.By("Views").Descending);
        }

        [Test]
        public void GetMostViewedMovies_ReturnsTop10()
        {
            var mostViewedMovies = _recommendationService!.GetMostViewedMovies();

            Assert.AreEqual(5, mostViewedMovies.Count);
            Assert.That(mostViewedMovies, Is.Ordered.By("Views").Descending);
        }

        [Test]
        public void GetRecommendedMovies_WhenNoWatchedMovies_ReturnsMostViewed()
        {
            var user = new UserMovie
            {
                WatchedMovies = new List<Movie>(),
                Watchlist = new List<Movie>()
            };

            var recommendedMovies = _recommendationService!.GetRecommendedMovies(user, ContentType.Movie);

            Assert.AreEqual(5, recommendedMovies.Count);
            Assert.That(recommendedMovies, Is.Ordered.By("Views").Descending);
        }

        [Test]
        public void GetRecommendedMovies_ByRating_ReturnsHighestRated()
        {
            var user = new UserMovie
            {
                WatchedMovies = new List<Movie>(),
                Watchlist = new List<Movie>()
            };

            var recommendedMovies = _recommendationService!.GetTopRatedMovies();

            Assert.That(recommendedMovies, Is.Ordered.By("Rating").Descending);
        }
    }
}
