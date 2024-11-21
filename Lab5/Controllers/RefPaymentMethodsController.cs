using Lab5.Models;
using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class RefPaymentMethodsController(ICustomApiService service) : Controller
{
    
    public async Task<IActionResult> Index()
    {
        return View(await service.GetAllAsync<RefPaymentMethod>());
    }
    
    public async Task<IActionResult> Details(int id)
    {
        return View(await service.GetAsync<RefPaymentMethod>(id));
    }
}