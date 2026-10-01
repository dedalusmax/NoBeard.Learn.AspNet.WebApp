using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebApp.Data.Entities;

namespace NoBeard.Learn.AspNet.WebApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Algebra.Movies2026;Trusted_Connection=True;MultipleActiveResultSets=true");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        base.OnModelCreating(modelBuilder);
    }
}
