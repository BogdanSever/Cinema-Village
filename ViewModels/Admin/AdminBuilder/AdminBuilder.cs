using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
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
        private readonly IMoviesAppService _movieAppService;

        public AdminBuilder(IUserAppService userAppService, IDirectorAppService directorAppService, ITheatreAppService theatreAppService, IMoviesAppService moviesAppService)
        {
            _userAppService = userAppService;
            _directorAppService = directorAppService;
            _theatreAppService = theatreAppService;
            _movieAppService = moviesAppService;
        }

        public AdminDahboardUserViewModel BuildAddUser()
        {
            return new AdminDahboardUserViewModel() { };
        }

        public AdminDahboardUserViewModel BuildDeleteUser()
        {
            var userAppModel = _userAppService.GetAllUsers();

            return new AdminDahboardUserViewModel()
            {
                Users = userAppModel
            };
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
            var movies = _movieAppService.GetAllMovies();

            return new AdminDashboardMovieViewModel() 
            { 
                Movies = movies,
                Directors = directorAppModel,
                Theatres = theatresAppModel
            };
        }

    }
}
