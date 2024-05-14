using CinemaVillage.AppModel.Movies;
using CinemaVillage.Services.MoviesAppService.Interface;

namespace CinemaVillage.ViewModels.AllMovie.AllMovieBuilder
{
    public class AllMovieBuilder
    {
        private readonly IMoviesAppService _movieAppService;

        public AllMovieBuilder(IMoviesAppService movieAppService)
        {
            _movieAppService = movieAppService;
        }

        public AllMovieViewModel Build()
        {
            var movieAppModel = _movieAppService.GetAllMoviesInNext30Days();

            return new AllMovieViewModel()
            {
                MovieAppModels = movieAppModel,
            };
        }
    }
}
