using Microsoft.EntityFrameworkCore;

namespace KnittingData;

public static class NeedleFunctions
{
	public static Task<List<Needle>> GetAllNeedlesAsync(this KnittingContext db)
	{
		return db.Needles.ToListAsync();
	}

	public static Task<Needle?> GetNeedleByIdAsync(this KnittingContext db, int id)
	{
		return db.Needles.FirstOrDefaultAsync(m => m.Id == id);
	}

	public static async Task CreateNeedleAsync(this KnittingContext db, Needle needle)
	{
		db.Needles.Add(needle);
		await db.SaveChangesAsync();
	}

	public static async Task UpdateNeedleAsync(this KnittingContext db, Needle needle)
	{
		db.Attach(needle).State = EntityState.Modified;
		try
		{
			await db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await db.NeedleExistsAsync(needle.Id))
			{
				throw new InvalidOperationException($"Needle with Id {needle.Id} not found.");
			}
			else
			{
				throw;
			}
		}
	}

	public static async Task DeleteNeedleAsync(this KnittingContext db, int id)
	{
		var needle = await db.Needles.FindAsync(id);
		if (needle != null)
		{
			db.Needles.Remove(needle);
			await db.SaveChangesAsync();
		}
	}

	public static Task<bool> NeedleExistsAsync(this KnittingContext db, int id)
	{
		return db.Needles.AnyAsync(e => e.Id == id);
	}
}
