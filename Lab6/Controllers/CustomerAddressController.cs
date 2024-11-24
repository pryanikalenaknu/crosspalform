using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerAddressController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerAddress>>> Get()
    {
        return await context.CustomerAddresses.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerAddress>> Get(int id)
    {
        var paymentMethod = await context.CustomerAddresses.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<CustomerAddress>> Create(CustomerAddress customerOrder)
    {
        context.CustomerAddresses.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.AddressId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.CustomerAddresses.Remove(await context.CustomerAddresses.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}