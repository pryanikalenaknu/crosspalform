using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RefOrderStatusCodesController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RefOrderStatusCode>>> Get()
    {
        return await context.RefOrderStatusCodes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RefOrderStatusCode>> Get(int id)
    {
        var paymentMethod = await context.RefOrderStatusCodes.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<RefOrderStatusCode>> Create(RefOrderStatusCode customerOrder)
    {
        context.RefOrderStatusCodes.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.OrderStatusCode }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.RefOrderStatusCodes.Remove(await context.RefOrderStatusCodes.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}