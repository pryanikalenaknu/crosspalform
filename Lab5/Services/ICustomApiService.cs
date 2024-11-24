using Lab5.Models;
using Lab6.Model;

namespace Lab5.Services;

public interface ICustomApiService
{
    
    string? GetBaseUrl();

    Task<string?> GetAccessToken();
    Task<List<T>?> GetAllAsync<T>();
    Task<T?> GetAsync<T>(int id);

    Task<List<SearchResponse>?> SearchOrdersAsync(SearchModel model);
    
    Task<Product?> GetProductAsync(int id, string apiVersion = "v1");

}