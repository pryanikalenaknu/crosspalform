using Lab5.Models;
using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class SearchController(ICustomApiService service) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Results(SearchModel query)
    {
        var results = await service.SearchOrdersAsync(query);
        
        return View(results);
    }
}