using CinemaVillage.Services.UserAppService.Interface;

namespace CinemaVillage.ViewModels.Admin.AdminBuilder
{
    public class AdminBuilder
    {
        private readonly IUserAppService _userAppService;

        public AdminBuilder(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public AdminDahboardViewModel BuildUpdateUser()
        {
            var userAppModel = _userAppService.GetAllUsers();

            return new AdminDahboardViewModel()
            {
                Users = userAppModel
            };
        }

    }
}
