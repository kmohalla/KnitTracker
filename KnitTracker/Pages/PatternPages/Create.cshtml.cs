using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.PatternPages;

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
    public Pattern Pattern { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _context.CreatePatternAsync(Pattern);

        return RedirectToPage("./Index");
    }
}
