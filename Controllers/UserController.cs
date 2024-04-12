using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.User.UserBuilder.UserFactory.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CinemaVillage.Controllers;

[OutputCache(NoStore = true, Duration = 0)]
[Authorize(Roles = "admin, user")]
public class UserController : Controller
{
    private readonly IUserAppService _userAppService;
    private readonly ILogger<UserController> _logger;
    private readonly IUserFactory _userFactory;

    public UserController(ILogger<UserController> logger, IUserAppService userAppService, IUserFactory userFactory)
    {
        _logger = logger;
        _userAppService = userAppService;
        _userFactory = userFactory;
    }

    [HttpGet("MyProfile")]
    public IActionResult Index()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid model state");
            }

            var builder = _userFactory.CreateBuilder();
            var model = builder.Build();

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return RedirectToAction("Error");
        }
    }

    public IActionResult DeleteUser()
    {
        try
        {
            var userStatus = _userAppService.GetUserStatus();
            if (userStatus != null && userStatus.Email != null)
            {
                _userAppService.DeleteUser(userStatus.Email);
            }
            else
            {
                throw new InvalidOperationException("User is null!");
            }

            return RedirectToAction("LogOut", "Access");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToAction("Error", "Home");
        }
    }
}
