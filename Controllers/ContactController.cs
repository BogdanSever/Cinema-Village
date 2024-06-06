using Microsoft.AspNetCore.Mvc;

namespace CinemaVillage.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
