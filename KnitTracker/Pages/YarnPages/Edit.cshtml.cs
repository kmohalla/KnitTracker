using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.YarnPages;

public class EditModel : YarnPageModel
{
	private readonly KnittingContext _context;

	public EditModel(KnittingContext context)
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

		//populate drop down list for brands
		await PopulateBrandNameDropDownListAsync(_context, yarn.BrandId);
		Yarn = yarn;
		return Page();
	}

	
	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		if (Yarn == null)
		{
			return NotFound();
		}
		var yarnToUpdate = await _context.GetYarnByIdAsync(Yarn.Id);

		if (yarnToUpdate == null)
		{
			return NotFound();
		}
		if (await TryUpdateModelAsync<Yarn>(
			yarnToUpdate,
			"yarn",   // Prefix for form value.
			y => y.Name, y => y.Fiber, y => y.Weight, y => y.ColorName, y => y.BrandId))
		{
			await _context.UpdateYarnAsync(yarnToUpdate);
			return RedirectToPage("./Index");
		}
		//Repopulate drop down list for brands. update.brandid determined selected item
		await PopulateBrandNameDropDownListAsync(_context, yarnToUpdate.BrandId);
		return Page();
	}
}
