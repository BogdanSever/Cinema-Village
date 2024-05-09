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

        public AdminFactory(IUserAppService userAppService, IDirectorAppService directorAppService, ITheatreAppService theatreAppService)
        {
            _userAppService = userAppService;
            _directorAppService = directorAppService;
            _theatreAppService = theatreAppService;
        }
        public AdminBuilder CreateBuilder()
        {
            return new(_userAppService, _directorAppService, _theatreAppService);
        }
    }
}
