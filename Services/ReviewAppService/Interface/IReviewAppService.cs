using CinemaVillage.AppModel.Reviews;
using CinemaVillage.Models;

namespace CinemaVillage.Services.ReviewAppService.Interface
{
    public interface IReviewAppService
    {
        void AddReview(Review reviewModel);
        List<ReviewsMoviePageAppModel> GetAllReviewsByMovieId(int movieId);
    }
}
