using Microsoft.EntityFrameworkCore;

namespace KnittingData;

public static class YarnFunctions
{
	public static Task<List<Yarn>> GetAllYarnsAsync(this KnittingContext db)
	{
		return db.Yarns.Include(y => y.Brand).ToListAsync();
	}

	public static Task<Yarn?> GetYarnByIdAsync(this KnittingContext db, int id)
	{
		return db.Yarns.FirstOrDefaultAsync(m => m.Id == id);
	}

	public static async Task CreateYarnAsync(this KnittingContext db, Yarn yarn)
	{
		db.Yarns.Add(yarn);
		await db.SaveChangesAsync();
	}

	public static async Task UpdateYarnAsync(this KnittingContext db, Yarn yarn)
	{
		db.Attach(yarn).State = EntityState.Modified;
		try
		{
			await db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await db.YarnExistsAsync(yarn.Id))
			{
				throw new InvalidOperationException($"Yarn with Id {yarn.Id} not found.");
			}
			else
			{
				throw;
			}
		}
	}

	public static async Task DeleteYarnAsync(this KnittingContext db, int id)
	{
		var yarn = await db.Yarns.FindAsync(id);
		if (yarn != null)
		{
			db.Yarns.Remove(yarn);
			await db.SaveChangesAsync();
		}
	}

	public static Task<bool> YarnExistsAsync(this KnittingContext db, int id)
	{
		return db.Yarns.AnyAsync(e => e.Id == id);
	}

	public static async Task<List<Brand>> GetBrandsForDropdownAsync(this KnittingContext db)
	{
		var brandQuery = from b in db.Brands
						 orderby b.Name
						 select b;
		return await brandQuery.AsNoTracking().ToListAsync();
	}
}
