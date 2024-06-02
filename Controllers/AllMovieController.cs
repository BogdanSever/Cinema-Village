using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.AllMovie.AllMovieBuilder.AllMovieFactory.Interface;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CinemaVillage.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class AllMovieController : Controller
    {
        private readonly ILogger<AllMovieController> _logger;
        private readonly IAllMovieFactory _allMovieFactory;

        public AllMovieController(ILogger<AllMovieController> logger, IAllMovieFactory allMovieFactory)
        {
            _logger = logger;
            _allMovieFactory = allMovieFactory;
        }

        public IActionResult Index()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("Invalid model state");
                }

                var builder = _allMovieFactory.CreateBuilder();
                var model = builder.Build();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}
