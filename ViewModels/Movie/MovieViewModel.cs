using CinemaVillage.AppModel.Movies;
using CinemaVillage.AppModel.Reviews;
using CinemaVillage.Services.ImdbApiService.Models;
using CinemaVillage.ViewModels.Review;

namespace CinemaVillage.ViewModels.Movie
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string DirectorName { get; set; }
        public List<string> Cast {  get; set; }
        public ImdbResponse Ratings { get; set; }
        public List<ReviewsMoviePageAppModel> Reviews { get; set; } 
        public List<MovieScheduleAppModel> MovieSchedule { get; set; }
    }
}
