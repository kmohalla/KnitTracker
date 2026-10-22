using Microsoft.AspNetCore.Mvc.Rendering;
using KnittingData;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace KnitTracker.Pages.ProjectPages
{
	public class ProjectPageModel:PageModel
	{
		public SelectList PatternNameSL { get; set; }
		public SelectList YarnNameSL { get; set; }
		public SelectList NeedleNameSL { get; set; }

		public async Task PopulatePatternNameDropDownListAsync(KnittingContext _context, object selectedPattern = null)
		{
			var patterns = await _context.GetPatternsForDropdownAsync();
			PatternNameSL = new SelectList(patterns, nameof(Pattern.Id), nameof(Pattern.Name), selectedPattern);
		}

		public async Task PopulateYarnNameDropDownListAsync(KnittingContext _context, object selectedYarn = null)
		{
			var yarns = await _context.GetYarnsForDropdownAsync();
			YarnNameSL = new SelectList(yarns, nameof(Yarn.Id), nameof(Yarn.Name), selectedYarn);
		}

		public async Task PopulateNeedleNameDropDownListAsync(KnittingContext _context, object selectedNeedle = null)
		{
			var needles = await _context.GetNeedlesForDropdownAsync();
			NeedleNameSL = new SelectList(needles, nameof(Needle.Id), nameof(Needle.SizeMm), selectedNeedle);
		}

	}
}
