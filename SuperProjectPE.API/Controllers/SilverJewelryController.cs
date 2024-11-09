using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SuperProjectPE.REPO.Abstract;
using SuperProjectPE.REPO.Services.SilverJewelry;

namespace SuperProjectPE.API.Controllers;

[ApiController]
[Route("odata/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class SilverJewelryController : ODataController
{
    private readonly ISilverJewelryService _silverJewelryService;

    public SilverJewelryController(ISilverJewelryService silverJewelryService)
    {
        _silverJewelryService = silverJewelryService;
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] Request.Create request)
    {
        try
        {
            await _silverJewelryService.CreateSJ(request);
            return Ok("Create Successfully !");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut()]
    public async Task<IActionResult> Put([FromBody] Request.Create request)
    {
        try
        {
            await _silverJewelryService.UpdateSJ(request);
            return Ok("Update Successfully !");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{silverJewelryId}")]
    public async Task<IActionResult> Delete(string silverJewelryId)
    {
        try
        {
            await _silverJewelryService.DeleteSJ(silverJewelryId);
            return Ok("Delete Successfully !");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [Authorize]
    [HttpGet()]
    [EnableQuery]
    public async Task<IActionResult> GetAll(string? searchTerm = null)
    {
        try
        {
            var result = await _silverJewelryService.GetAll(searchTerm);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("{key}")]
    [EnableQuery]
    public async Task<IActionResult> GetByKey(string key)
    {
        try
        {
            var result = await _silverJewelryService.GetById(key);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}