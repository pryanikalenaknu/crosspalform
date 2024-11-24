using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierAddressController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierAddress>>> Get()
    {
        return await context.SupplierAddresses.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierAddress>> Get(int id)
    {
        var paymentMethod = await context.SupplierAddresses.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<SupplierAddress>> Create(SupplierAddress customerOrder)
    {
        context.SupplierAddresses.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.AddressId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.SupplierAddresses.Remove(await context.SupplierAddresses.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}