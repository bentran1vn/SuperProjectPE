using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperProjectPE.REPO.Abstract;
using SuperProjectPE.REPO.Services.Identity;

namespace SuperProjectPE.API.Controllers;

[ApiController]
[Route("odata/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityServices _identityServices;

    public IdentityController(IIdentityServices identityServices)
    {
        _identityServices = identityServices;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Request.Login request)
    {
        try
        {
            var result = await _identityServices.Login(request.Email, request.Password);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    
    [HttpGet("me")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> GetMe()
    {
        
        return Ok("kakka");
    }
}