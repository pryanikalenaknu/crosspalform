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
    public async Task<ActionResult<IEnumerable<RefPaymentMethod>>> GetPaymentMethods()
    {
        return await context.RefPaymentMethods.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RefPaymentMethod>> GetPaymentMethod(int id)
    {
        var paymentMethod = await context.RefPaymentMethods.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
}