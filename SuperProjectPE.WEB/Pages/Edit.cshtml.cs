using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.Model;
using SuperProjectPE.WEB.API;

namespace SuperProjectPE.WEB.Pages;

public class Edit : PageModel
{
    private readonly ISilverJewelryService _service;

    public Edit(ISilverJewelryService service)
    {
        _service = service;
    }

    [BindProperty]
    public SilverJewelry Jewelry { get; set; }
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Jewelry = await _service.GetByIdAsync(id);
        Categories = await _service.GetCategoriesAsync();

        if (Jewelry == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            Console.WriteLine(Jewelry.SilverJewelryId);
            await _service.UpdateAsync(Jewelry);
            return RedirectToPage("./Home");
        }
        catch
        {
            ModelState.AddModelError("", "Error updating the jewelry. Please try again.");
            return Page();
        }
    }
}