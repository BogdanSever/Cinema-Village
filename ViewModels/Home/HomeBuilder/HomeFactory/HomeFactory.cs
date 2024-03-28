using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;

namespace CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory
{
    public class HomeFactory : IHomeFactory
    {
        private readonly IMoviesAppService _moviesAppService;
        private readonly IUserAppService _userAppService;

        public HomeFactory(IMoviesAppService moviesAppService, IUserAppService userAppService)
        {
            _moviesAppService = moviesAppService;
            _userAppService = userAppService; 
        }

        public HomeBuilder CreateBuilder()
        {
            return new(_moviesAppService, _userAppService);
        }
    }
}
