using CinemaVillage.AppModel.Movies;
using CinemaVillage.Services.BookingAppService.Interface;
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
        private readonly IBookingAppService _bookingAppService;
        private readonly IUserAppService _userAppService;

        public SeatSelectionController(ILogger<SeatSelectionController> logger, ISeatSelectionFactory seatSelectionFactory, 
                                       IMovieXrefTheatreAppService movieXrefTheatreAppService, IBookingAppService bookingAppService, 
                                       IUserAppService userAppService)
        {
            _logger = logger;
            _seatSelectionFactory = seatSelectionFactory;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
            _bookingAppService = bookingAppService;
            _userAppService = userAppService;
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
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpPost("SeatSelection/UpdateSeatsState")]
        public JsonResult UpdateSeatsState(string date, string hour, List<Seats> seats, int movieId, int theatreId, List<int> seatsBooked)
        {
            if (seats != null)
            {
                try
                {
                    var idMovieXrefTheatre = _movieXrefTheatreAppService.UpdateAvailability(date, hour, movieId, theatreId, seats);
                    var userId = _userAppService.GetConnectedUserData().Id;
                    _bookingAppService.AddBooking(idMovieXrefTheatre, userId, date, hour, seatsBooked);
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
