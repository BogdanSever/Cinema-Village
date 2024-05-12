using CinemaVillage.AppModel.Movies;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.Services.UserAppService;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;
using CinemaVillage.ViewModels.SeatSelection.SeatSelectionBuilder.SeatSelectionFactory.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CinemaVillage.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    [Authorize(Roles = "admin, user")]
    public class SeatSelectionController : Controller
    {
        private readonly ILogger<SeatSelectionController> _logger;
        private readonly ISeatSelectionFactory _seatSelectionFactory;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public SeatSelectionController(ILogger<SeatSelectionController> logger, ISeatSelectionFactory seatSelectionFactory, IMovieXrefTheatreAppService movieXrefTheatreAppService)
        {
            _logger = logger;
            _seatSelectionFactory = seatSelectionFactory;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }

        [HttpGet("SeatSelection/{date:required}/{hour:required}/{movieid:required}/{theatreid:required}")]
        public IActionResult Index(string date, string hour, int movieid, int theatreid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("Invalid model state");
                }

                var dateDecoded = Uri.UnescapeDataString(date);
                
                var builder = _seatSelectionFactory.CreateBuilder();
                var model = builder.Build(dateDecoded, hour, movieid, theatreid);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error");
            }
        }

        [HttpPost("SeatSelection/UpdateSeatsState")]
        public JsonResult UpdateSeatsState(string date, string hour, List<Seats> seats, int movieId, int theatreId)
        {
            if (seats != null)
            {
                try
                {
                    _movieXrefTheatreAppService.UpdateAvailability(date, hour, movieId, theatreId, seats);
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogError(ex.Message, ex);
                    throw new InvalidOperationException(ex.Message, ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    return new JsonResult("Error");
                }

                return new JsonResult("ok");
            }

            return new JsonResult("error");
        }
    }
}
