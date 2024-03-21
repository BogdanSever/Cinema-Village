using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;

namespace CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory
{
    public class HomeFactory : IHomeFactory
    {
        private readonly IMoviesAppService _moviesAppService;

        public HomeFactory(IMoviesAppService moviesAppService)
        {
            _moviesAppService = moviesAppService;
        }

        public HomeBuilder CreateBuilder()
        {
            return new(_moviesAppService);
        }
    }
}
