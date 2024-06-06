using Microsoft.AspNetCore.Mvc;

namespace CinemaVillage.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
