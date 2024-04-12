using CinemaVillage.Services.UserAppService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CinemaVillage.Controllers;

[OutputCache(NoStore = true, Duration = 0)]
public class UserController : Controller
{
    private readonly IUserAppService _userAppService;
    private readonly ILogger<UserController> _logger;   

    public UserController(ILogger<UserController> logger, IUserAppService userAppService)
    {
        _logger = logger;
        _userAppService = userAppService;
    }

    [HttpGet("MyProfile")]
    public IActionResult Index()
    {
        return View();
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
