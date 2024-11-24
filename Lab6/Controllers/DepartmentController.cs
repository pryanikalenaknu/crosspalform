using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> Get()
    {
        return await context.Departments.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> Get(int id)
    {
        var paymentMethod = await context.Departments.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<Department>> Create(Department customerOrder)
    {
        context.Departments.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.DepartmentId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.Departments.Remove(await context.Departments.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}