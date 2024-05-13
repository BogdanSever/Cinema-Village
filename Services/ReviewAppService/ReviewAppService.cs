using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.ReviewAppService.Interface;

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
    }
}
