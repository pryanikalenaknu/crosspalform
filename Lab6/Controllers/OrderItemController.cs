using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> Get()
    {
        return await context.OrderItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> Get(int id)
    {
        var paymentMethod = await context.OrderItems.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<OrderItem>> Create(OrderItem customerOrder)
    {
        context.OrderItems.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.OrderItemId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.OrderItems.Remove(await context.OrderItems.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}