using CinemaVillage.AppModel.Directors;
using CinemaVillage.AppModel.Movies;
using CinemaVillage.AppModel.Theatres;

namespace CinemaVillage.ViewModels.Admin
{
    public class AdminDashboardMovieViewModel
    {
        public MovieAddAppModel Movie {  get; set; }

        public List<DirectorsAppModel> Directors { get; set; }

        public List<TheatreAppModel> Theatres { get; set; }
    }
}
