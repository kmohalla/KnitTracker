using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.NeedlePages;

public class CreateModel : PageModel
{
    private readonly KnittingContext _context;

    public CreateModel(KnittingData.KnittingContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public KnittingData.Needle Needle { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _context.CreateNeedleAsync(Needle);

        return RedirectToPage("./Index");
    }
}
