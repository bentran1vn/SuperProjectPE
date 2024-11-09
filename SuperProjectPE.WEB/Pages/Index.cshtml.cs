using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperProjectPE.WEB.API;
using SuperProjectPE.WEB.Model;

namespace SuperProjectPE.WEB.Pages;

public class IndexModel : PageModel
{
    private readonly IIdentityService _identityService;

    public IndexModel(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [BindProperty]
    public LoginRequest LoginRequest { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await _identityService.LoginAsync(LoginRequest);
            return RedirectToPage("/Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return Page();
        }
    }
}