using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Schemes.Enums;

namespace Infrastructure.DbContext;

public class BackendDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseRequestConfiguration());
        base.OnModelCreating(modelBuilder);

    }
}
