using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Areas.Admin.Pages.BrandPages;

public class CreateModel : PageModel
{
    private readonly KnittingContext _context;

    public CreateModel(KnittingContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Brand Brand { get; set; } = default!;

  
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _context.CreateBrandAsync(Brand);

        return RedirectToPage("./Index");
    }
}
