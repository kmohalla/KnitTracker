using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.YarnPages;

public class CreateModel : YarnPageModel
{
    private readonly KnittingContext _context;

    public CreateModel(KnittingContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await PopulateBrandNameDropDownListAsync(_context);
        return Page();
    }

    [BindProperty]
    public Yarn Yarn { get; set; } = default!;

	
	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		var emptyYarn = new Yarn();
		if (await TryUpdateModelAsync<Yarn>(emptyYarn, "yarn", e => e.Name, e => e.Fiber, e => e.Weight, e => e.ColorName, e => e.BrandId))
		{
			await _context.CreateYarnAsync(emptyYarn);
			return RedirectToPage("./Index");
		}

		await PopulateBrandNameDropDownListAsync(_context, emptyYarn.BrandId);
		return Page();

	}
}
