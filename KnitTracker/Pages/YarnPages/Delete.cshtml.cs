using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.YarnPages;

public class DeleteModel : PageModel
{
    private readonly KnittingContext _context;

    public DeleteModel(KnittingContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Yarn Yarn { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var yarn = await _context.GetYarnByIdAsync(id.Value);
        if (yarn is null)
        {
            return NotFound();
        }
        else
        {
            Yarn = yarn;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        await _context.DeleteYarnAsync(id.Value);

        return RedirectToPage("./Index");
    }
}
