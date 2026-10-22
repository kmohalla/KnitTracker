using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.NeedlePages;

public class DeleteModel : PageModel
{
    private readonly KnittingContext _context;

    public DeleteModel(KnittingContext context)
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
        else
        {
            Needle = needle;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        await _context.DeleteNeedleAsync(id.Value);

        return RedirectToPage("./Index");
    }
}
