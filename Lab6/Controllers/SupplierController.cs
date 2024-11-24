using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Supplier>>> Get()
    {
        return await context.Suppliers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Supplier>> Get(int id)
    {
        var paymentMethod = await context.Suppliers.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<Supplier>> Create(Supplier customerOrder)
    {
        context.Suppliers.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.SupplierId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.Suppliers.Remove(await context.Suppliers.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}