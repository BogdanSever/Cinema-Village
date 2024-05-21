using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.TheatreAppService;
using CinemaVillage.Services.TheatreAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;

namespace CinemaVillage.ViewModels.Admin.AdminBuilder
{
    public class AdminBuilder
    {
        private readonly IUserAppService _userAppService;
        private readonly IDirectorAppService _directorAppService;
        private readonly ITheatreAppService _theatreAppService;  

        public AdminBuilder(IUserAppService userAppService, IDirectorAppService directorAppService, ITheatreAppService theatreAppService)
        {
            _userAppService = userAppService;
            _directorAppService = directorAppService;
            _theatreAppService = theatreAppService;
        }

        public AdminDahboardUserViewModel BuildAddUser()
        {
            return new AdminDahboardUserViewModel() { };
        }

        public AdminDahboardUserViewModel BuildUpdateUser()
        {
            var userAppModel = _userAppService.GetAllUsers();

            return new AdminDahboardUserViewModel()
            {
                Users = userAppModel
            };
        }

        public AdminDashboardMovieViewModel BuildForMovie()
        {
            var directorAppModel = _directorAppService.GetAllDirectors();
            var theatresAppModel = _theatreAppService.GetAllTheatres();

            return new AdminDashboardMovieViewModel() 
            { 
                Directors = directorAppModel,
                Theatres = theatresAppModel
            };
        }

    }
}
