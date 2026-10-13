using Microsoft.EntityFrameworkCore;

namespace KnittingData;

public static class YarnSearch
{
    // Search yarns by name, fiber, or color (any field may be blank).
    public static Task<List<Yarn>> SearchAsync(this KnittingContext db,
        string? name, string? fiber, string? color)
    {
        var q = db.Yarns.Include(y => y.Brand).AsQueryable();
        if (!string.IsNullOrWhiteSpace(name))  q = q.Where(y => y.Name.Contains(name));
        if (!string.IsNullOrWhiteSpace(fiber)) q = q.Where(y => y.Fiber.Contains(fiber));
        if (!string.IsNullOrWhiteSpace(color)) q = q.Where(y => y.ColorName.Contains(color));
        return q.OrderBy(y => y.Name).ToListAsync();
    }
}
