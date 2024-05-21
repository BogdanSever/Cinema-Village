using CinemaVillage.AppModel.Reviews;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.ReviewAppService.Interface;
using Microsoft.EntityFrameworkCore;

namespace CinemaVillage.Services.ReviewAppService
{
    public class ReviewAppService : IReviewAppService
    {
        private readonly CinemaDbContext _context;

        public ReviewAppService(CinemaDbContext context)
        {
            _context = context;
        }

        public void AddReview(Review reviewModel)
        {
            if (reviewModel != null)
            {
                try
                {
                    _context.Reviews.Add(reviewModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                _context.SaveChanges();
            }
        }

        public List<ReviewsMoviePageAppModel> GetAllReviewsByMovieId(int movieId)
        {
            List<ReviewsMoviePageAppModel> reviewsMoviePageAppModels = new List<ReviewsMoviePageAppModel>();

            var reviews = _context.Reviews.Where(r => r.IdMovie == movieId).ToList();
            foreach(var review in reviews)
            {
                var userName = _context.Users.Where(u => u.IdUser == review.IdUser).Select(u => u.FamilyName + " " + u.GivenName).FirstOrDefault();
                reviewsMoviePageAppModels.Add(new ReviewsMoviePageAppModel
                {
                    UserName = userName,
                    Review = review.Description,
                    NoOfStars = review.NoOfStars
                });
            }
            
            return reviewsMoviePageAppModels;
        }

        public void DeleteReviewsByMovieId(int movieId)
        {
            var reviewsToDelete = _context.Reviews.Where(r => r.IdMovie == movieId).ToList();

            foreach (var review in reviewsToDelete)
            {
                _context.Reviews.Where(r => r.IdReview == review.IdReview).ExecuteDelete();
            }
        }
    }
}
