using Lab6.Model;
using Lab6.Service;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesforceAuthController(ISalesforceService service) : ControllerBase
{
    
    [HttpPost("auth")]
    public async Task<IActionResult> Login([FromBody] SalesforceUserCred credentials)
    {
        try
        {
            var authResult =
                await service.AuthAsync(credentials);
            return Ok(new { authResult.AuthToken, authResult.InstanceUrl });
        }
        catch (Exception)
        {
            return Unauthorized("Authentication failed.");
        }
    }
}