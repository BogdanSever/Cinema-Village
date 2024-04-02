using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using CinemaVillage.Models;

namespace CinemaVillage.Controllers;

public class AccessController : Controller
{
    /*public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
    }*/

    public async Task<IActionResult> Login(User model)
    {
        List<Claim> claims = new List<Claim>() {
            new Claim(ClaimTypes.NameIdentifier, model.Email),
            new Claim(ClaimTypes.Role, model.Role)
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        AuthenticationProperties properties = new AuthenticationProperties()
        {
            AllowRefresh = true, 
            IsPersistent = true,
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
