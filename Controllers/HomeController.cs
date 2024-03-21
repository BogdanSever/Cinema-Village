using CinemaVillage.Models;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaVillage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeFactory _homeFactory;

        public HomeController(ILogger<HomeController> logger, IHomeFactory homeFactory)
        {
            _logger = logger;
            _homeFactory = homeFactory;
        }

        public IActionResult Index()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("Invalid model state");
                }

                var builder = _homeFactory.CreateBuilder();
                var model = builder.Build();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
