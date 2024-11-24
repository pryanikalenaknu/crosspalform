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
    public async Task<ActionResult<IEnumerable<CustomerOrder>>> Get()
    {
        return await context.CustomerOrders.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerOrder>> Get(int id)
    {
        var paymentMethod = await context.CustomerOrders.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }

    [HttpPost()]
    public async Task<ActionResult<CustomerOrder>> Create(CustomerOrder customerOrder)
    {
        context.CustomerOrders.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.OrderId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.CustomerOrders.Remove(await context.CustomerOrders.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}