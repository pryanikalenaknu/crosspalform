using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class ProductController(ICustomApiService service) : Controller
{
    
    public async Task<IActionResult> Index()
    {
        var v1 = await service.GetProductAsync(1, "v1");
        var v2 = await service.GetProductAsync(1, "v2");
        
        ViewData["v1"] = v1;
        ViewData["v2"] = v2;
        return View();
    }
    
}