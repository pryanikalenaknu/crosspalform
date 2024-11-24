using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffDepartmentController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StaffDepartmentAssignment>>> Get()
    {
        return await context.StaffDepartmentAssignments.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StaffDepartmentAssignment>> Get(int id)
    {
        var paymentMethod = await context.StaffDepartmentAssignments.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<StaffDepartmentAssignment>> Create(StaffDepartmentAssignment customerOrder)
    {
        context.StaffDepartmentAssignments.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.StaffId }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.StaffDepartmentAssignments.Remove(await context.StaffDepartmentAssignments.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}