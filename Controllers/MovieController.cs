using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;
using CinemaVillage.ViewModels.Movie.MovieBuilder.MovieFactory.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CinemaVillage.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieFactory _movieFactory;

        public MovieController(ILogger<MovieController> logger, IMovieFactory movieFactory)
        {
            _logger = logger;
            _movieFactory = movieFactory;
        }

        [Route("/Movie/{movieid}")]
        public async Task<IActionResult> Index(int movieid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("Invalid model state");
                }

                var builder = _movieFactory.CreateBuilder();
                var model = await builder.Build(movieid);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error");
            }
        }
    }
}
