using CinemaVillage.Services.ActorAppService.Interface;
using CinemaVillage.Services.ActorXrefMovieAppService.Interface;
using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.ImdbApiService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;

namespace CinemaVillage.ViewModels.Movie.MovieBuilder
{
    public class MovieBuilder
    {
        //image, title, director, cast, description
        private readonly IMoviesAppService _movieAppService;
        private readonly IDirectorAppService _directorAppService;
        private readonly IActorAppService _actorAppService;
        private readonly IActorXrefMovieAppService _actorXrefMovieAppService;
        private readonly IImdbApiService _imdbApiService;

        public MovieBuilder(IMoviesAppService movieAppService, IDirectorAppService directorAppService, 
                            IActorAppService actorAppService, IActorXrefMovieAppService actorXrefMovieAppService, 
                            IImdbApiService imdbApiService)
        {
            _movieAppService = movieAppService;
            _directorAppService = directorAppService;
            _actorAppService = actorAppService;
            _actorXrefMovieAppService = actorXrefMovieAppService;
            _imdbApiService = imdbApiService;
        }

        public async Task<MovieViewModel> Build(int movieId)
        {
            var movieAppModel = _movieAppService.GetMovieById(movieId);
            var directorName = _directorAppService.GetDirectorName(movieAppModel.IdDirector);
            var actorsIds = _actorXrefMovieAppService.GetAllActorsByMovieId(movieId);
            var cast = _actorAppService.GetCastByActorsIds(actorsIds);

            var ratings = await _imdbApiService.GetRatingAsync(movieAppModel.Title);

            return new MovieViewModel()
            {
                Title = movieAppModel.Title,
                Description = movieAppModel.Description,
                Image = movieAppModel.Image,
                DirectorName = directorName,
                Cast = cast,
                Ratings = ratings
            };
        }
    }
}
