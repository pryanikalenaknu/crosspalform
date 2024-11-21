using Lab5.Models;

namespace Lab5.Services;

public interface IAuthService
{

    Task<UserProfileModel> RegisterAsync(RegisterModel model, HttpContext context);
    Task<UserProfileModel> LoginAsync(UserLoginModel model, HttpContext context);

    Task<bool> LogoutAsync(HttpContext context);

    Task<string?> ApiAccessTokenGenerate();

}