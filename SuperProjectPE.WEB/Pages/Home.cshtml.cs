using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.Model;
using SuperProjectPE.WEB.API;

namespace SuperProjectPE.WEB.Pages;

public class HomeModel : PageModel
{
    private readonly ISilverJewelryService _service;
    private readonly IIdentityService _identityService;

    public HomeModel(ISilverJewelryService service, IIdentityService identityService)
    {
        _service = service;
        _identityService = identityService;
    }

    public IEnumerable<SilverJewelry> Jewelries { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var token = _identityService.GetTokenFromSession();
        if (token != null)
        {
            Jewelries = await _service.GetAllAsync(SearchTerm);
            return Page();
        }
        
        return RedirectToPage("/Index");
    }
}