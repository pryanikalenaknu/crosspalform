using Lab6.Data;
using Lab6.Data.Entities;
using Lab6.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchQuery request)
    {
        var query = _context.CustomerOrders
            .Include(o => o.OrderStatus)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .AsQueryable();

        if (request.StartDate.HasValue && request.EndDate.HasValue)
        {
            query = query.Where(ft => ft.OrderDate >= request.StartDate && ft.OrderDate <= request.EndDate);
        }

        if (request.ProductIds != null && request.ProductIds.Any())
        {
            query = query.Where(ft => ft.OrderItems.Any(or => request.ProductIds.Contains(or.Product.ProductId)));
        }

        if (!string.IsNullOrEmpty(request.OrderStart))
        {
            query = query.Where(ft => ft.OrderDetails.StartsWith(request.OrderStart));
        }

        var results = await query.ToListAsync();
        Console.WriteLine(results.Count);

        foreach (var i in results)
        {
            Console.WriteLine(i.OrderId);
        }
        return Ok(map(results));
    }

    private List<SearchResponse> map(List<CustomerOrder> orders)
    {
        return orders.Select(var => new SearchResponse()
            {
                CustomerId = var.CustomerId,
                FirstProductName = var.OrderItems.First().Product.ProductName,
                OrderDate = var.OrderDate,
                OrderDetails = var.OrderDetails,
                OrderId = var.OrderId,
                OrderStatus = var.OrderStatus.OrderStatusDescription,
                OrderStatusCode = var.OrderStatusCode,
                PaymentMethodCode = var.PaymentMethodCode
            })
            .ToList();
    }
}
