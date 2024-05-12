using CinemaVillage.Services.BookingAppService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;

namespace CinemaVillage.ViewModels.User.UserBuilder
{
    public class UserBuilder
    {
        private readonly IUserAppService _userAppService;
        private readonly IBookingAppService _bookingAppService;
        private readonly IMoviesAppService _moviesAppService;

        public UserBuilder(IUserAppService userAppService, IBookingAppService bookingAppService, IMoviesAppService moviesAppService)
        {
            _userAppService = userAppService;
            _bookingAppService = bookingAppService;
            _moviesAppService = moviesAppService;
        }

        public UserViewModel Build()
        {
            var userAppModel = _userAppService.GetConnectedUserData();
            var bookingAppModel = _bookingAppService.GetAllBookingsByUserID(userAppModel.Id);
            var (futureMovies, pastMovies, reviewedMovies) = _moviesAppService.GetMovies(bookingAppModel);

            return new UserViewModel()
            {
                FirstName = userAppModel.FirstName,
                LastName = userAppModel.LastName,
                OnGoingMovies = futureMovies,
                PastMovies = pastMovies,
                ReviewedMovies = reviewedMovies
            };
        }
    }
}
