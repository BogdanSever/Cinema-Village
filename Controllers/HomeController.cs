using CinemaVillage.AppModel.Users;
using CinemaVillage.Models;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;

namespace CinemaVillage.Controllers;

[OutputCache(NoStore = true, Duration = 0)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeFactory _homeFactory;
    private readonly IUserAppService _userAppService;

    public HomeController(ILogger<HomeController> logger, IHomeFactory homeFactory, IUserAppService userAppService)
    {
        _logger = logger;
        _homeFactory = homeFactory;
        _userAppService = userAppService;
    }

    public IActionResult Index()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _homeFactory.CreateBuilder();
            var model = builder.Build();

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error", new { errorMessage = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult SubmitFormSignUp(SignUpAppModel model)
    {
        if (model != null)
        {

            var userModel = new User
            {
                FamilyName = model.LastName,
                GivenName = model.FirstName,
                Email = model.Email,
                Password = model.Password,
                Role = "user"
            };

            try
            {
                _userAppService.AddUser(userModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Login", "Access", userModel);

        }
        else
        {
            return RedirectToAction("Error", new { errorMessage = "User is null! 500 error" });
        }
    }

    [HttpPost]
    public IActionResult SubmitFormLogIn(LogInAppModel model)
    {
        if (model != null)
        {
            User userToLogIn = null;
            try
            {
                if (_userAppService.CheckForUserExistance(model.Email))
                {
                    userToLogIn = _userAppService.GetUserByEmail(model.Email);
                }
                else
                {
                    throw new InvalidOperationException("User does not exists!");
                }
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Error", new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return RedirectToAction("Error", new { errorMessage = ex.Message });
            }

            return RedirectToAction("Login", "Access", userToLogIn);
        }
        else
        {
            return RedirectToAction("Error", new { errorMessage = "Error! 500 code" });
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet("Error")]
    public IActionResult Error(string errorMessage)
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errorMessage });
    }
}
