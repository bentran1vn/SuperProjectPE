using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SuperProjectPE.REPO.Abstract;

namespace SuperProjectPE.API.Controllers;

[ApiController]
[Route("odata/[controller]")]
public class CategoryController : ODataController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet()]
    [EnableQuery]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _categoryService.GetAllCate();
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}