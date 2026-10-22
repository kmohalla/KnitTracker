using Microsoft.EntityFrameworkCore;

namespace KnittingData;

public static class BrandFunctions
{
	public static Task<List<Brand>> GetAllBrandsAsync(this KnittingContext db)
	{
		return db.Brands.Include(b => b.Yarns).ToListAsync();
	}

	public static Task<Brand?> GetBrandByIdAsync(this KnittingContext db, int id)
	{
		return db.Brands.Include(b => b.Yarns).FirstOrDefaultAsync(m => m.Id == id);
	}

	public static async Task CreateBrandAsync(this KnittingContext db, Brand brand)
	{
		db.Brands.Add(brand);
		await db.SaveChangesAsync();
	}

	public static async Task UpdateBrandAsync(this KnittingContext db, Brand brand)
	{
		db.Attach(brand).State = EntityState.Modified;
		try
		{
			await db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await db.BrandExistsAsync(brand.Id))
			{
				throw new InvalidOperationException($"Brand with Id {brand.Id} not found.");
			}
			else
			{
				throw;
			}
		}
	}

	public static async Task DeleteBrandAsync(this KnittingContext db, int id)
	{
		var brand = await db.Brands.FindAsync(id);
		if (brand != null)
		{
			db.Brands.Remove(brand);
			await db.SaveChangesAsync();
		}
	}

	public static Task<bool> BrandExistsAsync(this KnittingContext db, int id)
	{
		return db.Brands.AnyAsync(e => e.Id == id);
	}
}
