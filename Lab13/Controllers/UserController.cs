using System.Security.Claims;
using Lab13.Exceptions;
using Lab13.Models;
using Lab13.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab13.Controllers;

public class UserController(IAuthService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegisterModel model)
    {
        try
        {
            await service.RegisterAsync(model, HttpContext);
            return Ok();
        }
        catch (UserRegistrationException e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody]UserLoginModel model)
    {
        try
        {
            var token = await service.LoginAsync(model, HttpContext);
            return Json(new
            {
                Token = token
            });
        }
        catch (UserLoginException e)
        {
           Console.WriteLine(e);
           return BadRequest();
        }
    }
    
    public async Task<IActionResult> Profile()
    {
        var token = Request.Headers.Authorization.ToString().Split(" ")[1];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var profile = await service.LoadUserProfile(token);
        if (profile == null)
        {
            return Unauthorized();
        }
        return Json(profile);
    }
}