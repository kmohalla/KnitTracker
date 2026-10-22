using Microsoft.EntityFrameworkCore;

namespace KnittingData;

public static class PatternFunctions
{
	public static Task<List<Pattern>> GetAllPatternsAsync(this KnittingContext db)
	{
		return db.Patterns.ToListAsync();
	}

	public static Task<Pattern?> GetPatternByIdAsync(this KnittingContext db, int id)
	{
		return db.Patterns.FirstOrDefaultAsync(m => m.Id == id);
	}

	public static async Task CreatePatternAsync(this KnittingContext db, Pattern pattern)
	{
		db.Patterns.Add(pattern);
		await db.SaveChangesAsync();
	}

	public static async Task UpdatePatternAsync(this KnittingContext db, Pattern pattern)
	{
		db.Attach(pattern).State = EntityState.Modified;
		try
		{
			await db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await db.PatternExistsAsync(pattern.Id))
			{
				throw new InvalidOperationException($"Pattern with Id {pattern.Id} not found.");
			}
			else
			{
				throw;
			}
		}
	}

	public static async Task DeletePatternAsync(this KnittingContext db, int id)
	{
		var pattern = await db.Patterns.FindAsync(id);
		if (pattern != null)
		{
			db.Patterns.Remove(pattern);
			await db.SaveChangesAsync();
		}
	}

	public static Task<bool> PatternExistsAsync(this KnittingContext db, int id)
	{
		return db.Patterns.AnyAsync(e => e.Id == id);
	}
}
