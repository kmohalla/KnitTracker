using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Areas.Admin.Pages.BrandPages;

public class IndexModel : PageModel
{
    private readonly KnittingContext _context;

    public IndexModel(KnittingContext context)
    {
        _context = context;
    }

    public IList<Brand> Brand { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Brand = await _context.GetAllBrandsAsync();
    }
}
