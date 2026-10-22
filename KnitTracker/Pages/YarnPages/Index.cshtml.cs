using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;
//using KnittingData.YarnSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnitTracker.Pages.YarnPages;

public class IndexModel : YarnPageModel
{
    private readonly KnittingContext _context;

    public IndexModel(KnittingContext context)
    {
        _context = context;
    }

    public IList<Yarn> Yarn { get; set; } = default!;

	public async Task OnGetAsync()
	{
		Yarn = await _context.GetAllYarnsAsync();

		if (!string.IsNullOrEmpty(NameSearchString) ||
			!string.IsNullOrEmpty(FiberSearchString) ||
			!string.IsNullOrEmpty(ColorSearchString))
		{
			Yarn = await _context.SearchAsync(
				name: NameSearchString,
				fiber: FiberSearchString,
				color: ColorSearchString);
		}
	}
}