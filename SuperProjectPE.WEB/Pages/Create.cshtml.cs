using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.Model;
using SuperProjectPE.WEB.API;

namespace SuperProjectPE.WEB.Pages;

public class Create : PageModel
{
    private readonly ISilverJewelryService _service;

    public Create(ISilverJewelryService service)
    {
        _service = service;
    }

    [BindProperty]
    public SilverJewelry Jewelry { get; set; }
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();

    public async Task OnGetAsync()
    {
        Categories = await _service.GetCategoriesAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        Jewelry.CreatedDate = DateTime.UtcNow;
        await _service.CreateAsync(Jewelry);
        return RedirectToPage("./Home");
    }
}