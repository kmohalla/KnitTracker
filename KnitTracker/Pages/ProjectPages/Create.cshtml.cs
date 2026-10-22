using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.ProjectPages;

public class CreateModel : ProjectPageModel
{
    private readonly KnittingContext _context;

    public CreateModel(KnittingContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await PopulatePatternNameDropDownListAsync(_context);
        await PopulateYarnNameDropDownListAsync(_context);
        await PopulateNeedleNameDropDownListAsync(_context);
        return Page();
    }

    [BindProperty]
    public Project Project { get; set; } = default!;

	
	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		var emptyProject = new Project();
		if (await TryUpdateModelAsync<Project>(emptyProject, "project", e => e.Name, e => e.Status, e => e.StartDate,e => e.PatternId, e => e.YarnId, e => e.NeedleId))
		{
			await _context.CreateProjectAsync(emptyProject);
			return RedirectToPage("./Index");
		}

		await PopulatePatternNameDropDownListAsync(_context, emptyProject.PatternId);
		await PopulateYarnNameDropDownListAsync(_context, emptyProject.YarnId);
		await PopulateNeedleNameDropDownListAsync(_context, emptyProject.NeedleId);
       
        return RedirectToPage("./Index");
    }
}
