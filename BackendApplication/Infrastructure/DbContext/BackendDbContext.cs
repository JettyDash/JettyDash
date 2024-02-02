using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext;

public class BackendDbContext(
    DbContextOptions<BackendDbContext> options,
    DbSet<User> users,
    DbSet<Connection> connections)
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<User> Users { get; set; } = users;
    public DbSet<Connection> Connections { get; set; } = connections;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ConnectionConfiguration());
        base.OnModelCreating(modelBuilder);

    }
}
