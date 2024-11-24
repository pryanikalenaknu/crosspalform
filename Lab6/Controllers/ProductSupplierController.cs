using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductSupplierController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductSupplier>>> Get()
    {
        return await context.ProductSuppliers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductSupplier>> Get(int id)
    {
        var paymentMethod = await context.ProductSuppliers.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<ProductSupplier>> Create(ProductSupplier customerOrder)
    {
        context.ProductSuppliers.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.SupplierId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.ProductSuppliers.Remove(await context.ProductSuppliers.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}