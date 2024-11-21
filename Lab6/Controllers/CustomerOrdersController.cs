using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerOrdersController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerOrder>>> GetPaymentMethods()
    {
        return await context.CustomerOrders.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerOrder>> GetPaymentMethod(int id)
    {
        var paymentMethod = await context.CustomerOrders.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
}