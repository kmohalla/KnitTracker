using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.PatternPages;

public class IndexModel : PageModel
{
    private readonly KnittingContext _context;

    public IndexModel(KnittingContext context)
    {
        _context = context;
    }

    public IList<Pattern> Patterns { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Patterns = await _context.GetAllPatternsAsync();
    }
}
