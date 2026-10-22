using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.PatternPages;

public class DeleteModel : PageModel
{
    private readonly KnittingContext _context;

    public DeleteModel(KnittingContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Pattern Pattern { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var pattern = await _context.GetPatternByIdAsync(id.Value);
        if (pattern is null)
        {
            return NotFound();
        }
        else
        {
            Pattern = pattern;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        await _context.DeletePatternAsync(id.Value);

        return RedirectToPage("./Index");
    }
}
