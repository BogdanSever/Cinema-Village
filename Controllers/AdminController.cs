using CinemaVillage.AppModel.Movies;
using CinemaVillage.AppModel.Users;
using CinemaVillage.Models;
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

    public AdminController(ILogger<AdminController> logger, IAdminFactory adminFactory, IUserAppService userAppService)
    {
        _logger = logger;
        _adminFactory = adminFactory;
        _userAppService = userAppService;
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
        var modelToSend = model;

        var movieImg = HttpContext.Request.Form.Files[0];

        /*byte[] bytes;
        using (var stream = new MemoryStream())
        {
            movieImg.CopyTo(stream);
            bytes = stream.ToArray();
        }*/

        modelToSend.Image = movieImg;

        //parse and verify form to not have multiple run date suprapuse in same theatre
        //generate json file of availabilty
        //add movie to table together with moviexreftheatre

        TempData["modelMovieAdd"] = JsonConvert.SerializeObject(modelToSend);

        return RedirectToAction("SelectDateAndTimeMovieAdd", new { theatre = modelToSend.TheatreName });
    }

    [HttpGet("MyAdminDashBoard/MovieAdd/DateAndTimeSelect")]
    public IActionResult SelectDateAndTimeMovieAdd(string theatre)
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
}
