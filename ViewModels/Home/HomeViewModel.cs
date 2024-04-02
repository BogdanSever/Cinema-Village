using CinemaVillage.AppModel.Movies;
using CinemaVillage.AppModel.Users;

namespace CinemaVillage.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<MovieAppModel> Movies { get; set; }

        public SignUpAppModel SignUpAppModel { get; set; }

        public LogInAppModel LogInAppModel { get; set; }

        public UserStatusAppModel LoggedInUserAppModel { get; set; }
    }
}
