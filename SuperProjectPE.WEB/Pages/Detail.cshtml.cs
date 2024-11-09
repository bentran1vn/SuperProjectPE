using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.Model;
using SuperProjectPE.WEB.API;

namespace SuperProjectPE.WEB.Pages;

public class Detail : PageModel
{
    private readonly ISilverJewelryService _service;
    private readonly IIdentityService _identityService;

    public Detail(ISilverJewelryService service, IIdentityService identityService)
    {
        _service = service;
        _identityService = identityService;
    }
    
    [BindProperty]
    public SilverJewelry Jewelry { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var token = _identityService.GetTokenFromSession();
        if (token != null)
        {
            if (id == null)
            {
                return NotFound();
            }

            Jewelry = await _service.GetByIdAsync(id);

            if (Jewelry == null)
            {
                return NotFound();
            }

            return Page();
        }
        
        return RedirectToPage("/Index");
        
    }
}