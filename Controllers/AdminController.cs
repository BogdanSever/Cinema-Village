using CinemaVillage.AppModel.Movies;
using CinemaVillage.AppModel.Users;
using CinemaVillage.Models;
using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.HelperService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Admin.AdminBuilder.AdminFactory.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Newtonsoft.Json;

namespace CinemaVillage.Controllers;

[OutputCache(NoStore = true, Duration = 0)]
[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly IAdminFactory _adminFactory;
    private readonly IUserAppService _userAppService;
    private readonly IDirectorAppService _directorAppService;
    private readonly IMoviesAppService _moviesAppService;
    private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;
    private readonly IJsonCreatorService _jsonCreatorService;

    public AdminController(ILogger<AdminController> logger, IAdminFactory adminFactory, IUserAppService userAppService, 
                           IDirectorAppService directorAppService, IMoviesAppService moviesAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService, 
                           IJsonCreatorService jsonCreatorService)
    {
        _logger = logger;
        _adminFactory = adminFactory;
        _userAppService = userAppService;
        _directorAppService = directorAppService;
        _moviesAppService = moviesAppService;
        _movieXrefTheatreAppService = movieXrefTheatreAppService;
        _jsonCreatorService = jsonCreatorService;
    }

    [HttpGet("MyAdminDashBoard")]
    public IActionResult Index()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error");
        }
    }

    [HttpGet("MyAdminDashBoard/UserAdd")]
    public IActionResult UserAdd() 
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildAddUser();

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult SubmitFormUserAdd([Bind(Prefix = "UpdatedUser")] UpdateUserAppModel model)
    {
        if (model != null)
        {

            var userModel = new User
            {
                FamilyName = model.LastName,
                GivenName = model.FirstName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            try
            {
                _userAppService.AddUser(userModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            throw new InvalidOperationException("User is null");
        }
    }

    [HttpGet("MyAdminDashBoard/UserDelete")]
    public IActionResult UserDelete(string userId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildDeleteUser();

            if (!string.IsNullOrEmpty(userId))
            {
                ViewBag.SelectedItem = userId;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult SubmitSearchDeleteUser(string selectedItem)
    {
        return RedirectToAction("UserDelete", new { userId = selectedItem });
    }

    [HttpPost]
    public IActionResult SubmitFormDeleteUser([Bind(Prefix = "Item1")] UpdateUserAppModel model)
    {
        if (model != null)
        {

            var userModel = new User
            {
                IdUser = model.Id,
                FamilyName = model.LastName,
                GivenName = model.FirstName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            try
            {
                _userAppService.DeleteUser(userModel.Email);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            throw new InvalidOperationException("User is null");
        }
    }

    [HttpGet("MyAdminDashBoard/UserUpdate")]
    public IActionResult UserUpdate(string userId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildUpdateUser();

            if (!string.IsNullOrEmpty(userId))
            {
                ViewBag.SelectedItem = userId;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult SubmitSearchUpdateUser(string selectedItem)
    {
        return RedirectToAction("UserUpdate", new { userId = selectedItem });
    }

    [HttpPost]
    public IActionResult SubmitFormUpdateUser([Bind(Prefix = "Item1")] UpdateUserAppModel model)
    {
        if (model != null)
        {

            var userModel = new User
            {
                IdUser = model.Id,
                FamilyName = model.LastName,
                GivenName = model.FirstName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            try
            {
                _userAppService.UpdateUser(userModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error");
            }

            return RedirectToAction("UserUpdate", "Admin");

        }
        else
        {
            throw new InvalidOperationException("User is null");
        }
    }

    [HttpGet("MyAdminDashBoard/UserListAll")]
    public IActionResult UserListAll()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildUpdateUser();

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error");
        }
    }

    [HttpGet("MyAdminDashBoard/MovieAdd")]
    public IActionResult MovieAdd()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildForMovie();

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error");
        }
    }

    [HttpPost("MyAdminDashBoard/SubmitFormMovieAdd")]
    public IActionResult SubmitFormMovieAdd([Bind(Prefix = "Movie")] MovieAddAppModel model)
    {
        if (model != null)
        {
            var movieImg = HttpContext.Request.Form.Files[0];

            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                movieImg.CopyTo(stream);
                bytes = stream.ToArray();
            }

            var idDirector = _directorAppService.GetDirectorId(model.DirectorName);
            int movieId;

            var movieModel = new Movie
            {
                IdDirector = idDirector,
                Title = model.Title,
                Genre = model.Genre,
                Duration = model.Duration,
                ReleaseDate = model.ReleaseDate,
                Discription = model.Description,
                Image = bytes
            };

            try
            {
                movieId = _moviesAppService.AddMovie(movieModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error");
            }

            return RedirectToAction("SelectDateAndTimeMovieAdd", new { theatreID = model.TheatreName, movieID = movieId });
        }
        else
        {
            return RedirectToAction("Error");
        }
    }

    [HttpGet("MyAdminDashBoard/MovieAdd/DateAndTimeSelect")]
    public IActionResult SelectDateAndTimeMovieAdd(string theatreID, int movieID)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            TempData["theatreID"] = theatreID;
            TempData["movieID"] = movieID;

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error");
        }
    }

    [HttpPost("MyAdminDashBoard/MovieAdd/CurrentAvailableDates")]
    public JsonResult CurrentAvailableDates(string theatreID)
    {
        try
        {
            var dictDatesAndHours = new Dictionary<string, List<string>>();
            var theatre_id = Int32.Parse(theatreID);
            var availabilties = _movieXrefTheatreAppService.GetAvailabilty(theatre_id);

            foreach(var availabilty in availabilties)
            {
                var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(availabilty);
                foreach(var entry in model)
                {
                    var hours = new List<string>();
                    foreach(var hourRunning in entry.HoursRunning)
                    {
                        hours.Add(hourRunning.Hour);
                    }

                    if (!dictDatesAndHours.ContainsKey(entry.Date))
                    {
                        dictDatesAndHours.Add(entry.Date, hours);
                    }
                    else
                    {
                        dictDatesAndHours[entry.Date].AddRange(hours);
                    }
                }
            }

            return Json(new
            {
                Status = "OK",
                Message = "Response Added",
                MyDictionary = dictDatesAndHours
            });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                Status = "ERROR",
                Message = ex.Message.ToString(),
                Data = new int[0]
            });
        }
    }

    [HttpPost("MyAdminDashBoard/MovieAdd/SubmitMovieAdd")]
    public string SubmitMovieAdd(List<MovieAddJsonAppModel> json)
    {
        if (json != null)
        {
            var availabilityJson = _jsonCreatorService.CreateJson(json);
            var theatreID = Int32.Parse(TempData["theatreID"].ToString());
            var movieID = Int32.Parse(TempData["movieID"].ToString());

            var movieXrefTheatreModel = new MovieXrefTheatre
            {
                IdMovie = movieID,
                IdTheatre = theatreID,
                RunningDatetime = DateTime.Now,
                Availability = availabilityJson
            };

            try
            {
                _movieXrefTheatreAppService.AddMovieXrefTheatre(movieXrefTheatreModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return "Error";
            }

            return "MyAdminDashBoard";
        }
        else
        {
            return "Error";
        }
    }
}
