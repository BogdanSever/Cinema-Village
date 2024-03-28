using Microsoft.AspNetCore.Mvc;

namespace CinemaVillage.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet("MyAdminDashBoard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
