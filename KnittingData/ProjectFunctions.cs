using Microsoft.EntityFrameworkCore;

namespace KnittingData;

public static class ProjectFunctions
{
	public static Task<List<Project>> GetAllProjectsAsync(this KnittingContext db)
	{
		return db.Projects.Include(p => p.Pattern).Include(p => p.Yarn).Include(p => p.Needle).ToListAsync();
	}

	public static Task<Project?> GetProjectByIdAsync(this KnittingContext db, int id)
	{
		return db.Projects.Include(p => p.Pattern).Include(p => p.Yarn).Include(p => p.Needle).FirstOrDefaultAsync(m => m.Id == id);
	}

	public static async Task CreateProjectAsync(this KnittingContext db, Project project)
	{
		db.Projects.Add(project);
		await db.SaveChangesAsync();
	}

	public static async Task UpdateProjectAsync(this KnittingContext db, Project project)
	{
		db.Attach(project).State = EntityState.Modified;
		try
		{
			await db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await db.ProjectExistsAsync(project.Id))
			{
				throw new InvalidOperationException($"Project with Id {project.Id} not found.");
			}
			else
			{
				throw;
			}
		}
	}

	public static async Task DeleteProjectAsync(this KnittingContext db, int id)
	{
		var project = await db.Projects.FindAsync(id);
		if (project != null)
		{
			db.Projects.Remove(project);
			await db.SaveChangesAsync();
		}
	}

	public static Task<bool> ProjectExistsAsync(this KnittingContext db, int id)
	{
		return db.Projects.AnyAsync(e => e.Id == id);
	}

	public static async Task<List<Pattern>> GetPatternsForDropdownAsync(this KnittingContext db)
	{
		var patternQuery = from p in db.Patterns
						   orderby p.Name
						   select p;
		return await patternQuery.AsNoTracking().ToListAsync();
	}

	public static async Task<List<Yarn>> GetYarnsForDropdownAsync(this KnittingContext db)
	{
		var yarnQuery = from y in db.Yarns
						orderby y.Name
						select y;
		return await yarnQuery.AsNoTracking().ToListAsync();
	}

	public static async Task<List<Needle>> GetNeedlesForDropdownAsync(this KnittingContext db)
	{
		var needleQuery = from n in db.Needles
						  orderby n.SizeMm, n.LengthCm
						  select n;
		return await needleQuery.AsNoTracking().ToListAsync();
	}
}
