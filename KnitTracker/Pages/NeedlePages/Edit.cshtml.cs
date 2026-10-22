using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.NeedlePages;

public class EditModel : PageModel
{
    private readonly KnittingContext _context;

    public EditModel(KnittingContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Needle Needle { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var needle = await _context.GetNeedleByIdAsync(id.Value);
        if (needle is null)
        {
            return NotFound();
        }
        Needle = needle;
        return Page();
    }

    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _context.UpdateNeedleAsync(Needle);

        return RedirectToPage("./Index");
    }
}
