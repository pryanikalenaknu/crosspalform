using System.Security.Claims;
using Lab5.Exceptions;
using Lab5.Models;
using Lab5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class UserController(IAuthService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        try
        {
            await service.RegisterAsync(model, HttpContext);
        }
        catch (UserRegistrationException e)
        {
            Console.WriteLine(e);
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }
    
    public IActionResult Register()
    {
        return View(new RegisterModel());
    }
    
    public IActionResult Login()
    {
        return View(new UserLoginModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginModel model)
    {
        try
        {
            await service.LoginAsync(model, HttpContext);
            return RedirectToAction("Index", "Home");
        }
        catch (UserLoginException e)
        {
           Console.WriteLine(e);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await service.LogoutAsync(HttpContext);
        return RedirectToAction("Login");
    }
    
    [Authorize]
    public IActionResult Profile()
    {
        var claims = new List<Claim>(User.Claims);
       
        return View(new UserProfileModel()
        {
            UserName = claims[1].Value,
            Email = claims[0].Value,
            FullUserName = claims[2].Value,
            Phone = claims[3].Value
        });
    }
}