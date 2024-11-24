using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RefJobTitleController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RefJobTitle>>> Get()
    {
        return await context.RefJobTitles.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RefJobTitle>> Get(int id)
    {
        var paymentMethod = await context.RefJobTitles.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<RefJobTitle>> Create(RefJobTitle customerOrder)
    {
        context.RefJobTitles.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.JobTitleCode }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.RefJobTitles.Remove(await context.RefJobTitles.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}