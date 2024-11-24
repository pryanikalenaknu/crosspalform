using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RefPaymentMethodsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RefPaymentMethod>>> Get()
    {
        return await context.RefPaymentMethods.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RefPaymentMethod>> Get(int id)
    {
        var paymentMethod = await context.RefPaymentMethods.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
    
    [HttpPost()]
    public async Task<ActionResult<RefPaymentMethod>> Create(RefPaymentMethod customerOrder)
    {
        context.RefPaymentMethods.Add(customerOrder);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customerOrder.PaymentMethodCode }, customerOrder);
    }
    
    [HttpDelete()]
    public async Task<ActionResult> Delete(int id)
    {
        context.RefPaymentMethods.Remove(await context.RefPaymentMethods.FindAsync(id));
        await context.SaveChangesAsync();
        return Ok();
    }
}