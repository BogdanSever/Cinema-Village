using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.ViewModels.Review.ReviewBuilder.ReviewFactory.Interface;

namespace CinemaVillage.ViewModels.Review.ReviewBuilder.ReviewFactory
{
    public class ReviewFactory : IReviewFactory
    {
        private readonly IMoviesAppService _movieAppService;

        public ReviewFactory(IMoviesAppService movieAppService)
        {
            _movieAppService = movieAppService;
        }

        public ReviewBuilder CreateBuilder()
        {
            return new(_movieAppService);
        }
    }
}
