using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentStoreChainController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentStoreChain>>> Get()
    {
        return await context.DepartmentStoreChains.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentStoreChain>> Get(int id)
    {
        var paymentMethod = await context.DepartmentStoreChains.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<DepartmentStoreChain>> Create(DepartmentStoreChain customerOrder)
    {
        context.DepartmentStoreChains.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.DeptStoreChainId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.DepartmentStoreChains.Remove(await context.DepartmentStoreChains.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}