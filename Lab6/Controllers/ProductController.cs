using Lab6.Data;
using Lab6.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult GetProductV1(int id)
    {
        var product = _context.Products.First(product => product.ProductId == id);
        return Ok(product);
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetProductV2(int id)
    {
        var product = _context.Products
            
            .Include(p => p.ProductType)
            .First(product => product.ProductId == id);
        return Ok(product);
    }
    
    [HttpPost()]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Product>> Create(Product customerOrder)
    {
        _context.Products.Add(customerOrder);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductV1), new { id = customerOrder.ProductId }, customerOrder);
    }
    
    [HttpDelete()]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> Delete(int id)
    {
        _context.Products.Remove(await _context.Products.FindAsync(id));
        await _context.SaveChangesAsync();
        return Ok();
    }
}