using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Contexts;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }

    // public DbSet<SuperHero> SuperHeroes { get; set; }
}