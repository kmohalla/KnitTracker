using Microsoft.EntityFrameworkCore;

namespace KnittingData;

public class KnittingContext : DbContext
{
    public KnittingContext(DbContextOptions<KnittingContext> options) : base(options) { }

    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Yarn> Yarns => Set<Yarn>();
    public DbSet<Needle> Needles => Set<Needle>();
    public DbSet<Pattern> Patterns => Set<Pattern>();
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Needle>().Property(n => n.SizeMm).HasPrecision(4, 2);
        b.Entity<Project>().Property(p => p.StartDate).HasColumnType("date");
    }
}
