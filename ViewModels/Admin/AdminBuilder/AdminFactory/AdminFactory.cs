using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.TheatreAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Admin.AdminBuilder.AdminFactory.Interface;

namespace CinemaVillage.ViewModels.Admin.AdminBuilder.AdminFactory
{
    public class AdminFactory : IAdminFactory
    {
        private readonly IUserAppService _userAppService;
        private readonly IDirectorAppService _directorAppService;
        private readonly ITheatreAppService _theatreAppService;
        private readonly IMoviesAppService _movieAppService;

        public AdminFactory(IUserAppService userAppService, IDirectorAppService directorAppService, ITheatreAppService theatreAppService, IMoviesAppService movieAppService)
        {
            _userAppService = userAppService;
            _directorAppService = directorAppService;
            _theatreAppService = theatreAppService;
            _movieAppService = movieAppService;
        }
        public AdminBuilder CreateBuilder()
        {
            return new(_userAppService, _directorAppService, _theatreAppService, _movieAppService);
        }
    }
}
