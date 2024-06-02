using CinemaVillage.Models;
using CinemaVillage.Services.ReviewAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Review.ReviewBuilder.ReviewFactory.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CinemaVillage.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    [Authorize(Roles = "admin, user")]
    public class ReviewController : Controller
    {
        private readonly IReviewFactory _reviewFactory;
        private readonly ILogger<ReviewController> _logger;
        private readonly IUserAppService _userAppService;
        private readonly IReviewAppService _reviewAppService;

        public ReviewController(ILogger<ReviewController> logger, IReviewFactory reviewFactory, IUserAppService userAppService, IReviewAppService reviewAppService)
        {
            _logger = logger;
            _reviewFactory = reviewFactory;
            _userAppService = userAppService;
            _reviewAppService = reviewAppService;
        }

        [HttpGet("Review/{movieid:required}")]
        public IActionResult Index(int movieId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("Invalid model state");
                }

                var builder = _reviewFactory.CreateBuilder();
                var model = builder.Build(movieId);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpPost("Review/SubmitReview")]
        public string SubmitReview(string review, int noOfStars, int movieId)
        {
            try
            {
                var userConnectedId = _userAppService.GetConnectedUserData().Id;

                var reviewModel = new Review
                {
                    IdMovie = movieId,
                    IdUser = userConnectedId,
                    Description = review,
                    NoOfStars = decimal.Parse(noOfStars.ToString()),
                };

                _reviewAppService.AddReview(reviewModel);

                return "OK";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "Error";
            }
        }
    }
}
