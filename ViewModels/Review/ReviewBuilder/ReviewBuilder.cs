using CinemaVillage.Services.MoviesAppService.Interface;

namespace CinemaVillage.ViewModels.Review.ReviewBuilder
{
    public class ReviewBuilder
    {
        private readonly IMoviesAppService _movieAppService;

        public ReviewBuilder(IMoviesAppService movieAppService)
        {
            _movieAppService = movieAppService;
        }

        public ReviewViewModel Build(int movieid)
        {
            var movieAppModel = _movieAppService.GetMovieById(movieid);

            return new ReviewViewModel()
            {
                Title = movieAppModel.Title,
                Image = movieAppModel.Image
            };
        }
    }
}
