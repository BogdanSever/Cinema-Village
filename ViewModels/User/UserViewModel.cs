using CinemaVillage.AppModel.Movies;

namespace CinemaVillage.ViewModels.User
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<MovieUserPageAppModel> OnGoingMovies { get; set; }
        public List<MovieUserPageAppModel> PastMovies { get; set; }
    }
}
