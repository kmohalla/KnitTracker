using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.ProjectPages;

public class IndexModel : PageModel
{
    private readonly KnittingContext _context;

    public IndexModel(KnittingContext context)
    {
        _context = context;
    }

    public IList<Project> Project { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Project = await _context.GetAllProjectsAsync();
    }
}
