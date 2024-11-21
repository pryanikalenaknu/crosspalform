using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RefOrderStatusCodesController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RefOrderStatusCode>>> GetPaymentMethods()
    {
        return await context.RefOrderStatusCodes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RefOrderStatusCode>> GetPaymentMethod(int id)
    {
        var paymentMethod = await context.RefOrderStatusCodes.FindAsync(id);

        if (paymentMethod == null)
        {
            return NotFound();
        }

        return paymentMethod;
    }
}