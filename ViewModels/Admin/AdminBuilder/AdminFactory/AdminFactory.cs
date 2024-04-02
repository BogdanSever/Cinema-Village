using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Admin.AdminBuilder.AdminFactory.Interface;

namespace CinemaVillage.ViewModels.Admin.AdminBuilder.AdminFactory
{
    public class AdminFactory : IAdminFactory
    {
        private readonly IUserAppService _userAppService;

        public AdminFactory(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        public AdminBuilder CreateBuilder()
        {
            return new(_userAppService);
        }
    }
}
