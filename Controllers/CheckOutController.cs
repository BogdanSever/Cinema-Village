using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.CheckOut.CheckOutBuilder.CheckOutFactory.Interface;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CinemaVillage.Controllers
{

    [OutputCache(NoStore = true, Duration = 0)]
    [Authorize(Roles = "admin, user")]
    public class CheckOutController : Controller
    {
        private readonly ILogger<CheckOutController> _logger;
        private readonly ICheckOutFactory _checkOutFactory;

        public CheckOutController(ILogger<CheckOutController> logger, ICheckOutFactory checkOutFactory)
        {
            _logger = logger;
            _checkOutFactory = checkOutFactory;
        }

        [HttpGet("CheckOut")]
        public IActionResult Index(string date, string hour, string movieID, string theatreID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("Invalid model state");
                }

                var builder = _checkOutFactory.CreateBuilder();
                var model = builder.Build(date, hour, movieID, theatreID);

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
