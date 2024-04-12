using CinemaVillage.Services.BookingAppService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.User.UserBuilder.UserFactory.Interface;

namespace CinemaVillage.ViewModels.User.UserBuilder.UserFactory
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserAppService _userAppService;
        private readonly IBookingAppService _bookingAppService;
        private readonly IMoviesAppService _moviesAppService;

        public UserFactory(IUserAppService userAppService, IBookingAppService bookingAppService, IMoviesAppService moviesAppService)
        {
            _userAppService = userAppService;
            _bookingAppService = bookingAppService;
            _moviesAppService = moviesAppService;
        }

        public UserBuilder CreateBuilder()
        {
            return new(_userAppService, _bookingAppService, _moviesAppService);
        }
    }
}
