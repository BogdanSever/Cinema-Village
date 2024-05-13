using CinemaVillage.Services.ImdbApiService.Models;

namespace CinemaVillage.ViewModels.Movie
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string DirectorName { get; set; }
        public List<string> Cast {  get; set; }
        public ImdbResponse Ratings { get; set; }
    }
}
