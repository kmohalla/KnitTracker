using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnittingData;

namespace KnitTracker.Pages.ProjectPages;

public class EditModel : ProjectPageModel
{
    private readonly KnittingContext _context;

    public EditModel(KnittingContext context)
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

        await PopulateNeedleNameDropDownListAsync(_context,project.NeedleId);
        await PopulatePatternNameDropDownListAsync(_context, project.PatternId);
        await PopulateYarnNameDropDownListAsync(_context, project.YarnId);

        Project = project;
        return Page();
    }

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		if (Project == null)
		{
			return NotFound();
		}

		var projectToUpdate = await _context.GetProjectByIdAsync(Project.Id);

		if (projectToUpdate == null)
		{
			return NotFound();
		}
		if (await TryUpdateModelAsync<Project>(projectToUpdate, "project", 
			p => p.Name, p=> p.Status, p => p.StartDate, p => p.PatternId, p => p.YarnId, p => p.NeedleId))
		{
			await _context.UpdateProjectAsync(projectToUpdate);
			return RedirectToPage("./Index");
		}

		//Repopulate drop down lists
		await PopulateNeedleNameDropDownListAsync(_context, projectToUpdate.NeedleId);
		await PopulatePatternNameDropDownListAsync(_context, projectToUpdate.PatternId);
		await PopulateYarnNameDropDownListAsync(_context, projectToUpdate.YarnId);
		return Page();

	}
}
