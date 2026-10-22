using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Areas.Admin.Pages.BrandPages;

public class DeleteModel : PageModel
{
    private readonly KnittingContext _context;

    public DeleteModel(KnittingContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Brand Brand { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var brand = await _context.GetBrandByIdAsync(id.Value);
        if (brand is null)
        {
            return NotFound();
        }
        else
        {
            Brand = brand;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        await _context.DeleteBrandAsync(id.Value);

        return RedirectToPage("./Index");
    }
}
