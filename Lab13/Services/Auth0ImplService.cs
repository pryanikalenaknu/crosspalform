using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Lab13;
using Lab13.Exceptions;
using Lab13.Models;
using Lab13.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Lab13.Services;

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
        Console.WriteLine(model.Password);
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
        
        return userModel;
    }

    public async Task<string> LoginAsync(UserLoginModel model, HttpContext context)
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
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        var responseContent = await response.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<JsonElement>(responseContent).GetProperty(AccessTokenKey).GetString();
        return token;
    }


    public async Task<UserProfileModel?> LoadUserProfile(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BearerScheme, accessToken);
        var response = await _httpClient.GetAsync($"https://{ApplicationContext.Domain}/userinfo");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        if (!response.IsSuccessStatusCode)
            return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonElement>(responseContent);

        var userModel = new UserProfileModel
        {
            Email = json.GetProperty(EmailKey).GetString(),
            
        };

        return userModel;
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

    public async Task<bool> CheckToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }
        var profile = await LoadUserProfile(token.Split(" ")[1]);
        return profile != null;
    }
}
