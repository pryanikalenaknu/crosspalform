using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentStoreController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentStore>>> Get()
    {
        return await context.DepartmentStores.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentStore>> Get(int id)
    {
        var paymentMethod = await context.DepartmentStores.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<DepartmentStore>> Create(DepartmentStore customerOrder)
    {
        context.DepartmentStores.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.DeptStoreId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.DepartmentStores.Remove(await context.DepartmentStores.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}