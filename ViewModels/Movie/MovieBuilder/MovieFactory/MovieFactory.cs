using CinemaVillage.Services.ActorAppService.Interface;
using CinemaVillage.Services.ActorXrefMovieAppService.Interface;
using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.ImdbApiService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.Services.ReviewAppService.Interface;
using CinemaVillage.ViewModels.Movie.MovieBuilder.MovieFactory.Interface;

namespace CinemaVillage.ViewModels.Movie.MovieBuilder.MovieFactory
{
    public class MovieFactory : IMovieFactory
    {
        private readonly IMoviesAppService _movieAppService;
        private readonly IDirectorAppService _directorAppService;
        private readonly IActorAppService _actorAppService;
        private readonly IActorXrefMovieAppService _actorXrefMovieAppService;
        private readonly IImdbApiService _imdbApiService;
        private readonly IReviewAppService _reviewsAppService;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public MovieFactory(IMoviesAppService movieAppService, IDirectorAppService directorAppService, IActorAppService actorAppService, 
                            IActorXrefMovieAppService actorXrefMovieAppService, IImdbApiService imdbApiService, IReviewAppService reviewAppService,
                            IMovieXrefTheatreAppService movieXrefTheatreAppService)
        {
            _movieAppService = movieAppService;
            _directorAppService = directorAppService;
            _actorAppService = actorAppService;
            _actorXrefMovieAppService = actorXrefMovieAppService;
            _imdbApiService = imdbApiService;
            _reviewsAppService = reviewAppService;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }

        public MovieBuilder CreateBuilder()
        {
            return new(_movieAppService, _directorAppService, _actorAppService, _actorXrefMovieAppService, _imdbApiService, _reviewsAppService, _movieXrefTheatreAppService);
        }
    }
}
