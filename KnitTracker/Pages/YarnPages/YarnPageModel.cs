using KnittingData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace KnitTracker.Pages.YarnPages
{
	public class YarnPageModel:PageModel
	{
		public SelectList BrandNameSL { get; set; }

		public async Task PopulateBrandNameDropDownListAsync(KnittingContext _context, object selectedBrand = null)
		{
			var brands = await _context.GetBrandsForDropdownAsync();
			BrandNameSL = new SelectList(brands, nameof(Brand.Id), nameof(Brand.Name), selectedBrand);
		}

		[BindProperty(SupportsGet=true)]
		public string? NameSearchString { get; set; }

		[BindProperty(SupportsGet = true)]
		public string? FiberSearchString { get; set; }
		[BindProperty(SupportsGet = true)]
		public string? ColorSearchString { get; set; }

	}

}
