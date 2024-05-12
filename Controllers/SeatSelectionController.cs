using Microsoft.AspNetCore.Mvc;

namespace CinemaVillage.Controllers
{
    public class SeatSelectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
