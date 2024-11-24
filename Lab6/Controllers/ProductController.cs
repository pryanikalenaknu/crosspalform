using Lab6.Data;
using Lab6.Data.Entities;
using Lab6.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace Lab6.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class ProductController(ApplicationDbContext _context, ISalesforceService _service) : ControllerBase
{

    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult GetProductV1()
    {
        var product = _context.Products.First();
        return Ok(product);
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetProductV2()
    {
        var product = _context.Products
            
            .Include(p => p.ProductType)
            .First();
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
    
    [HttpPost("sendSalesforceData")]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult> SendAddresses()
    {
        var products = await _context.Products.ToListAsync();
        var salesforceObjects = products.Select(p => new
        {
            Id = p.ProductId,  
            Name__c = p.ProductName,  
            ProductTypeCode__c = p.ProductTypeCode
        }).ToList();


        try
        {
            var authResult = await _service.AuthAsync(ApplicationContext.DefaultUserCred);
        
            var accessToken = authResult.AuthToken;  
            var instanceUrl = authResult.InstanceUrl;
        
            var client = new RestClient($"{instanceUrl}/services/data/v57.0/sobjects/Product/");
        
            foreach (var product in salesforceObjects)
            {
                var body = JsonConvert.SerializeObject(product); 
                
                var request = new RestRequest(Method.POST);
                
                request.AddHeader("Authorization", "Bearer " + accessToken);  
                request.AddHeader("Content-Type", "application/json"); 
                request.AddJsonBody(body);  
                
                var response = await client.ExecuteAsync(request);
                
                if (!response.IsSuccessful)
                {
                    return BadRequest($"Error sending data to Salesforce: {response.Content}");
                }
            }
        
            return Ok("Data successfully sent to Salesforce.");
        }
        catch (Exception e)
        {
            return BadRequest($"Error sending data to Salesforce: {e}");
        }
    }
    
}