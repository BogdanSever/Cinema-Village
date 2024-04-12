using CinemaVillage.AppModel.Users;
using CinemaVillage.Models;

namespace CinemaVillage.Services.UserAppService.Interface
{
    public interface IUserAppService
    {
        void AddUser(User userModel);
        bool CheckForUserExistance(string email);
        User GetUserByEmail(string email);
        UserStatusAppModel GetUserStatus();
        void DeleteUser(string email);
        List<UserAppModel> GetAllUsers();
        void UpdateUser(User userModel);
        UserAppModel GetConnectedUserData();
    }
}
