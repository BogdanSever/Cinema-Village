using CinemaVillage.AppModel.Users;

namespace CinemaVillage.ViewModels.Admin;

public class AdminDahboardViewModel
{
    public List<UserAppModel> Users { get; set; }

    public UpdateUserAppModel UpdatedUser { get; set; }

}
