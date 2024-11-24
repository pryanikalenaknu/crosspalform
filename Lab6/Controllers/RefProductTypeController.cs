using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RefProductTypeController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RefProductType>>> Get()
    {
        return await context.RefProductTypes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RefProductType>> Get(int id)
    {
        var paymentMethod = await context.RefProductTypes.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<RefProductType>> Create(RefProductType customerOrder)
    {
        context.RefProductTypes.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.ProductTypeCode }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.RefProductTypes.Remove(await context.RefProductTypes.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}