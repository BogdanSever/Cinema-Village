using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Program.ProgramBuilder.ProgramFactory.Interface;
using CinemaVillage.ViewModels.User.UserBuilder.UserFactory.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Drawing;

namespace CinemaVillage.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class ProgramController : Controller
    {
        private readonly ILogger<ProgramController> _logger;
        private readonly IProgramFactory _programFactory;

        public ProgramController(ILogger<ProgramController> logger, IProgramFactory programFactory)
        {
            _logger = logger;
            _programFactory = programFactory;
        }

        [HttpGet("Program")]
        public IActionResult Index(string date)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("Invalid model state");
                }

                if (string.IsNullOrEmpty(date))
                {
                    date = DateOnly.FromDateTime(DateTime.Now).ToString();
                }

                var builder = _programFactory.CreateBuilder();
                var model = builder.Build(date);

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
