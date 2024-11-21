using Lab5.Models;
using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class CustomerOrdersController(ICustomApiService service) : Controller
{
    
    public async Task<IActionResult> Index()
    {
        return View(await service.GetAllAsync<CustomerOrder>());
    }
    
    public async Task<IActionResult> Details(int id)
    {
        return View(await service.GetAsync<CustomerOrder>(id));
    }
}