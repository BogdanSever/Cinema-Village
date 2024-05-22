using CinemaVillage.AppModel.Directors;

namespace CinemaVillage.ViewModels.Admin
{
    public class AdminDashboardDirectorViewModel
    {
        public List<DirectorsAppModel> Directors {  get; set; } 
        public DirectorAddAppModel DirectorAddAppModel {  get; set; }
    }
}
