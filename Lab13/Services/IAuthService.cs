using Lab13.Models;

namespace Lab13.Services;

public interface IAuthService
{

    Task<UserProfileModel> RegisterAsync(RegisterModel model, HttpContext context);
    Task<string> LoginAsync(UserLoginModel model, HttpContext context);
    
    Task<UserProfileModel?> LoadUserProfile(string accessToken);

    Task<bool> CheckToken(string accessToken);
}