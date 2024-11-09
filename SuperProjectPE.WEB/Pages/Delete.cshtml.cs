using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.Model;
using SuperProjectPE.WEB.API;

namespace SuperProjectPE.WEB.Pages;

public class Delete : PageModel
{
    private readonly ISilverJewelryService _service;

    public Delete(ISilverJewelryService service)
    {
        _service = service;
    }

    [BindProperty]
    public SilverJewelry Jewelry { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
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

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            await _service.DeleteAsync(id);
            return RedirectToPage("./Home");
        }
        catch
        {
            return Page();
        }
    }
}