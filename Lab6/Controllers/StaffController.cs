using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Staff>>> Get()
    {
        return await context.Staff.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Staff>> Get(int id)
    {
        var paymentMethod = await context.Staff.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<Staff>> Create(Staff customerOrder)
    {
        context.Staff.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.StaffId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.Staff.Remove(await context.Staff.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}