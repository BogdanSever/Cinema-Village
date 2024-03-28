using CinemaVillage.Services.MoviesAppService.Interface;

namespace CinemaVillage.ViewModels.Home.HomeBuilder
{
    public class HomeBuilder
    {

        private readonly IMoviesAppService _moviesAppService;

        public HomeBuilder(IMoviesAppService moviesAppService)
        {
            _moviesAppService = moviesAppService;
        }

        public HomeViewModel Build()
        {
            var moviesAppModel = _moviesAppService.GetImagesMovies();

            return new HomeViewModel
            {
                Movies = moviesAppModel
            };

        }

    }
}
