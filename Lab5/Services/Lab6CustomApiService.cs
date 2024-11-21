using System.Net.Http.Headers;
using Azure;
using Lab5.Models;
using Lab6.Model;

namespace Lab5.Services;

public class Lab6CustomApiService(HttpClient httpClient, IAuthService service) : ICustomApiService
{
    public string? GetBaseUrl()
    {
        return ApplicationContext.ApiHost;
    }

    public async Task<string?> GetAccessToken()
    {
        return await service.ApiAccessTokenGenerate();
    }

    public async Task<List<T>?> GetAllAsync<T>()
    {
        var token = await GetAccessToken();
        Console.WriteLine(token);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await httpClient.GetAsync(GetBaseUrl() + "/api/" + typeof(T).Name + "s");
        response.EnsureSuccessStatusCode();
        
        var res = await response.Content.ReadFromJsonAsync<ApiResponseListModel<T>>();
        
        return res?.Result;
    }

    public async Task<T?> GetAsync<T>(int id)
    {
        var token = await GetAccessToken();
        Console.WriteLine(token);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync(GetBaseUrl() + "/api/" + typeof(T).Name + "s/" + id);
        response.EnsureSuccessStatusCode();
        
        var res = await response.Content.ReadFromJsonAsync<T>();
        
        return res;
    }


    public async Task<List<SearchResponse>?> SearchOrdersAsync(SearchModel model)
    {
        var token = await GetAccessToken();
        Console.WriteLine(token);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.PostAsJsonAsync(GetBaseUrl() + "/api/Search/search", model);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        var res = await response.Content.ReadFromJsonAsync<ApiResponseListModel<SearchResponse>>();

        return res?.Result;
    }
}