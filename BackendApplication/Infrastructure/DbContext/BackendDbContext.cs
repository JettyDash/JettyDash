using Infrastructure.Entity;
using Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext;

public class BackendDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Connection> Connections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ConnectionConfiguration());
        modelBuilder.SeedUsers(); // Call the seeding method
        modelBuilder.SeedConnections(); // Call the seeding method

        base.OnModelCreating(modelBuilder);

    }
}
