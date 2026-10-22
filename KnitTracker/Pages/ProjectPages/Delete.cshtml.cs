using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.ProjectPages;

public class DeleteModel : PageModel
{
    private readonly KnittingContext _context;

    public DeleteModel(KnittingContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Project Project { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var project = await _context.GetProjectByIdAsync(id.Value);
        if (project is null)
        {
            return NotFound();
        }
        else
        {
            Project = project;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        await _context.DeleteProjectAsync(id.Value);

        return RedirectToPage("./Index");
    }
}
