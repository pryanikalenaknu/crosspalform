using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> Get()
    {
        return await context.Addresses.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Address>> Get(int id)
    {
        var paymentMethod = await context.Addresses.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<Address>> Create(Address customerOrder)
    {
        context.Addresses.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.AddressId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.Addresses.Remove(await context.Addresses.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}