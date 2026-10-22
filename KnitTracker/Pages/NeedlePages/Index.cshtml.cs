using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.NeedlePages;

public class IndexModel : PageModel
{
    private readonly KnittingContext _context;

    public IndexModel(KnittingContext context)
    {
        _context = context;
    }

    public IList<Needle> Needle { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Needle = await _context.GetAllNeedlesAsync();
    }
}
