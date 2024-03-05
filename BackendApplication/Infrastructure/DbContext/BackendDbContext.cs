using Infrastructure.Entities;
using Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext;

public class BackendDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
    {
        //DropCreateDatabaseIfModelChanges 
        // Database.SetInitializer<BackendDbContext>(new CreateDatabaseIfNotExists<SchoolDBContext>());
        // options.SetInitializer<BackendDbContext>(new SchoolDBInitializer());
        // Database.SetInitializer<BackendDbContext>(new DropCreateDatabaseIfModelChanges<BackendDbContext>());


    }

    public DbSet<User> Users { get; set; }
    public DbSet<Connection> Connections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ConnectionConfiguration());
        // modelBuilder.SeedUsers(); // Call the seeding method
        // modelBuilder.SeedConnections(); // Call the seeding method

        base.OnModelCreating(modelBuilder);

    }
}
