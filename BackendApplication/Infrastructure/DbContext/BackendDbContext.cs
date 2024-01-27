using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext;

public class BackendDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<HostConnection> HostConnections { get; set; }
    public DbSet<UrlConnection> UrlConnections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new HostConnectionConfiguration());
        modelBuilder.ApplyConfiguration(new UrlConnectionConfiguration());
        
        base.OnModelCreating(modelBuilder);

    }
}
