using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;

namespace CinemaVillage.ViewModels.Home.HomeBuilder
{
    public class HomeBuilder
    {

        private readonly IMoviesAppService _moviesAppService;
        private readonly IUserAppService _userAppService;

        public HomeBuilder(IMoviesAppService moviesAppService, IUserAppService userAppService)
        {
            _moviesAppService = moviesAppService;
            _userAppService = userAppService;
        }

        public HomeViewModel Build()
        {
            var moviesAppModel = _moviesAppService.GetAllMovies();
            var userLoggedIn = _userAppService.GetUserStatus();

            return new HomeViewModel
            {
                Movies = moviesAppModel,
                LoggedInUserAppModel = userLoggedIn
            };

        }

    }
}
