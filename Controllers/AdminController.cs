using CinemaVillage.AppModel.Directors;
using CinemaVillage.AppModel.Movies;
using CinemaVillage.AppModel.Users;
using CinemaVillage.Models;
using CinemaVillage.Services.ActorXrefMovieAppService.Interface;
using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.HelperService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.Services.ReviewAppService.Interface;
using CinemaVillage.Services.TheatreAppService.Interface;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Admin.AdminBuilder.AdminFactory.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.IdentityModel.Tokens;
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
    private readonly IReviewAppService _reviewAppService;
    private readonly IActorXrefMovieAppService _actorXrefMovieAppService;
    private readonly ITheatreAppService _theatreAppService;

    public AdminController(ILogger<AdminController> logger, IAdminFactory adminFactory, IUserAppService userAppService,
                           IDirectorAppService directorAppService, IMoviesAppService moviesAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService,
                           IJsonCreatorService jsonCreatorService, IReviewAppService reviewAppService, IActorXrefMovieAppService actorXrefMovieAppService,
                           ITheatreAppService theatreAppService)
    {
        _logger = logger;
        _adminFactory = adminFactory;
        _userAppService = userAppService;
        _directorAppService = directorAppService;
        _moviesAppService = moviesAppService;
        _movieXrefTheatreAppService = movieXrefTheatreAppService;
        _jsonCreatorService = jsonCreatorService;
        _reviewAppService = reviewAppService;
        _actorXrefMovieAppService = actorXrefMovieAppService;
        _theatreAppService = theatreAppService;
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
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }

    #region User
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
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
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
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "User is null" });
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
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
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
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "User is null" });
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
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
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
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "User is null" });
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
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }
    #endregion

    #region Movies
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
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
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
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("SelectDateAndTimeMovieAdd", new { theatreID = model.TheatreName, movieID = movieId });
        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "Movie is null" });
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
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
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

            foreach (var availabilty in availabilties)
            {
                var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(availabilty);
                foreach (var entry in model)
                {
                    var hours = new List<string>();
                    foreach (var hourRunning in entry.HoursRunning)
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

    [HttpGet("MyAdminDashBoard/MovieDelete")]
    public IActionResult MovieDelete(string movieId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildForMovie();

            if (!string.IsNullOrEmpty(movieId))
            {
                ViewBag.SelectedItem = movieId;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult SubmitSearchDeleteMovie(string selectedItem)
    {
        return RedirectToAction("MovieDelete", new { movieId = selectedItem });
    }

    [HttpPost]
    public IActionResult SubmitFormDeleteMovie([Bind(Prefix = "Item1")] MovieAddAppModel model)
    {
        if (model != null)
        {

            var movieModel = new Movie
            {
                IdMovie = model.Id,
                Genre = model.Genre,
                Duration = model.Duration,
                ReleaseDate = model.ReleaseDate,
                Discription = model.Description
            };

            try
            {
                _reviewAppService.DeleteReviewsByMovieId(movieModel.IdMovie);
                _actorXrefMovieAppService.DeleteActorsXrefMovieByMovieId(movieModel.IdMovie);
                _movieXrefTheatreAppService.DeleteMovieXrefTheatreByMovieId(movieModel.IdMovie);
                _moviesAppService.DeleteMovieByMovieId(movieModel.IdMovie);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "User is null" });
        }
    }

    [HttpGet("MyAdminDashBoard/MovieUpdate")]
    public IActionResult MovieUpdate(string movieId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildForMovie();

            if (!string.IsNullOrEmpty(movieId))
            {
                ViewBag.SelectedItem = movieId;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult SubmitSearchUpdateMovie(string selectedItem)
    {
        return RedirectToAction("MovieUpdate", new { movieId = selectedItem });
    }

    [HttpPost]
    public IActionResult SubmitFormUpdateMovie([Bind(Prefix = "Item1")] MovieAddAppModel model)
    {
        if (model != null)
        {

            var movieModel = new Movie
            {
                IdMovie = model.Id,
                Title = model.Title,
                Genre = model.Genre,
                Duration = model.Duration,
                ReleaseDate = model.ReleaseDate,
                Discription = model.Description
            };

            try
            {
                _moviesAppService.UpdateMovie(movieModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "User is null" });
        }
    }

    [HttpGet("MyAdminDashBoard/MovieListAll")]
    public IActionResult MovieListAll()
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
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }
    #endregion

    #region Theatres
    [HttpGet]
    public IActionResult TheatreAdd()
    {
        var theatreModel = new Theatre
        {
            Capacity = 40,
            NoOfRows = 6
        };

        try
        {
            _theatreAppService.AddTheatre(theatreModel);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("MyAdminDashBoard/TheatreDelete")]
    public IActionResult TheatreDelete(string theatreId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildDeleteTheatre();

            if (!string.IsNullOrEmpty(theatreId))
            {
                ViewBag.SelectedItem = theatreId;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult SubmitSearchDeleteTheatre(string selectedItem)
    {
        return RedirectToAction("TheatreDelete", new { theatreId = selectedItem });
    }

    [HttpPost]
    public IActionResult SubmitFormDeleteTheatre([Bind(Prefix = "Item1")] MovieAddAppModel model)
    {
        if (model != null)
        {

            var theatreModel = new Theatre
            {
                IdTheatre = Int32.Parse(model.TheatreName),
            };

            try
            {
                _movieXrefTheatreAppService.DeleteMovieXrefTheatreByTheatreId(theatreModel.IdTheatre);
                _theatreAppService.DeleteTheatre(theatreModel.IdTheatre);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "User is null" });
        }
    }

    #endregion

    #region Directors

    [HttpGet("MyAdminDashBoard/DirectorAdd")]
    public IActionResult DirectorAdd()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildAddDirector();

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult SubmitFormDirectorAdd([Bind(Prefix = "DirectorAddAppModel")] DirectorAddAppModel model)
    {
        if (model != null)
        {

            var directorModel = new Director
            {
                GivenName = model.FirstName,
                FamilyName = model.LastName,
            };

            try
            {
                _directorAppService.AddDirector(directorModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "User is null" });
        }
    }

    [HttpGet("MyAdminDashBoard/DirectorDelete")]
    public IActionResult DirectorDelete(string directorId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildAddDirector();

            if (!string.IsNullOrEmpty(directorId))
            {
                ViewBag.SelectedItem = directorId;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult SubmitSearchDeleteDirector(string selectedItem)
    {
        return RedirectToAction("DirectorDelete", new { directorId = selectedItem });
    }

    [HttpPost]
    public IActionResult SubmitFormDeleteDirector([Bind(Prefix = "Item1")] DirectorAddAppModel model)
    {
        if (model != null)
        {
            var directorModel = new Director
            {
                IdDirector = model.Id,
                FamilyName = model.LastName,
                GivenName = model.FirstName
            };

            try
            {
                _directorAppService.DeleteDirector(directorModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "Director is null" });
        }
    }

    [HttpGet("MyAdminDashBoard/DirectorUpdate")]
    public IActionResult DirectorUpdate(string directorId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildAddDirector();

            if (!string.IsNullOrEmpty(directorId))
            {
                ViewBag.SelectedItem = directorId;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult SubmitSearchUpdateDirector(string selectedItem)
    {
        return RedirectToAction("DirectorUpdate", new { directorId = selectedItem });
    }

    [HttpPost]
    public IActionResult SubmitFormUpdateDirector([Bind(Prefix = "Item1")] DirectorAddAppModel model)
    {
        if (model != null)
        {

            var directorModel = new Director
            {
                IdDirector = model.Id,
                FamilyName = model.LastName,
                GivenName = model.FirstName,
            };

            try
            {
                _directorAppService.UpdateDirector(directorModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Index", "Admin");

        }
        else
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "Director is null" });
        }
    }

    [HttpGet("MyAdminDashBoard/DirectorListAll")]
    public IActionResult DirectorListAll()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _adminFactory.CreateBuilder();
            var model = builder.BuildAddDirector();

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
        }
    }

    #endregion
}
