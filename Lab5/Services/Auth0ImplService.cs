using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Lab5.Exceptions;
using Lab5.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Lab5.Services;

public class Auth0ImplService : IAuthService
{
    private const string BearerScheme = "Bearer";
    private const string ContentType = "application/json";
    private const string GrantTypePassword = "password";
    private const string GrantTypeClientCredentials = "client_credentials";
    private const string OpenIdScope = "openid profile email";
    private const string ConnectionType = "Username-Password-Authentication";
    private const string UserMetadataKey = "user_metadata";
    private const string EmailKey = "email";
    private const string FullNameKey = "fullname";
    private const string PhoneKey = "phonenumber";
    private const string UsernameKey = "username";
    private const string AccessTokenKey = "access_token";

    private readonly HttpClient _httpClient;

    public Auth0ImplService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<UserProfileModel> RegisterAsync(RegisterModel model, HttpContext context)
    {
        var token = await ApiAccessTokenGenerate();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BearerScheme, token);

        var content = new StringContent(JsonSerializer.Serialize(new
        {
            email = model.Email,
            password = model.Password,
            connection = ConnectionType,
            user_metadata = new
            {
                username = model.Username,
                fullname = model.FullName,
                phonenumber = model.PhoneNumber
            }
        }), Encoding.UTF8, ContentType);

        var response = await _httpClient.PostAsync($"https://{ApplicationContext.Domain}/api/v2/users", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new UserRegistrationException("Помилка при реєстрації людей");
        }
        
        var userModel = new UserProfileModel
        {
            Email = model.Email,
            FullUserName = model.FullName,
            Phone = model.PhoneNumber,
            UserName = model.Username
        };

        LoginUser(context, userModel);
        
        return userModel;
    }

    public async Task<UserProfileModel> LoginAsync(UserLoginModel model, HttpContext context)
    {
        var content = new StringContent(JsonSerializer.Serialize(new
        {
            grant_type = GrantTypePassword,
            username = model.Email,
            password = model.Password,
            audience = $"https://{ApplicationContext.Domain}/api/v2/",
            client_id = ApplicationContext.Id,
            client_secret = ApplicationContext.Secret,
            scope = OpenIdScope
        }), Encoding.UTF8, ContentType);

        var response = await _httpClient.PostAsync($"https://{ApplicationContext.Domain}/oauth/token", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new UserLoginException("Помилка при авторизації користувача");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<JsonElement>(responseContent).GetProperty(AccessTokenKey).GetString();

        var userModel = await LoadUserProfile(token);

        LoginUser(context, userModel);
        return userModel;
    }
    
    public async Task<bool> LogoutAsync(HttpContext context)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return true;
    }

    private async Task<UserProfileModel?> LoadUserProfile(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BearerScheme, accessToken);
        var response = await _httpClient.GetAsync($"https://{ApplicationContext.Domain}/userinfo");

        if (!response.IsSuccessStatusCode)
            throw new UserLoginException($"Cannot get user info for token {accessToken}");

        var responseContent = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonElement>(responseContent);

        var userModel = new UserProfileModel
        {
            Email = json.GetProperty(EmailKey).GetString(),
            FullUserName = json.GetProperty(UserMetadataKey).GetProperty(FullNameKey).GetString(),
            Phone = json.GetProperty(UserMetadataKey).GetProperty(PhoneKey).GetString(),
            UserName = json.GetProperty(UserMetadataKey).GetProperty(UsernameKey).GetString()
        };

        return userModel;
    }
    
    private async void LoginUser(HttpContext context, UserProfileModel model)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, model.Email),
            new(UsernameKey, model.UserName ?? ""),
            new(FullNameKey, model.FullUserName ?? ""),
            new(PhoneKey, model.Phone ?? "")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);
    }
    
    private async Task<string?> ApiAccessTokenGenerate()
    {
        var content = new StringContent(JsonSerializer.Serialize(new
        {
            client_id = ApplicationContext.Id,
            client_secret = ApplicationContext.Secret,
            audience = $"https://{ApplicationContext.Domain}/api/v2/",
            grant_type = GrantTypeClientCredentials
        }), Encoding.UTF8, ContentType);

        var response = await _httpClient.PostAsync($"https://{ApplicationContext.Domain}/oauth/token", content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<JsonElement>(responseContent);
        return token.GetProperty(AccessTokenKey).GetString();
    }
}
