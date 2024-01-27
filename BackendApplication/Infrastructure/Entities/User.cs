using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entities;

public class User
{
    public int UserId { get; set; } // Primary key
    public string Username { get; set; }
    public string Password { get; set; }
    public int PasswordRetryCount { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<HostConnection> HostConnections { get; set; }
    public virtual ICollection<UrlConnection> UrlConnections { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);
        
        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.HasIndex(u => u.Username).IsUnique();

        builder.Property(u => u.Password).IsRequired().HasMaxLength(255);

        builder.Property(u => u.PasswordRetryCount).IsRequired();

        builder.Property(u => u.IsActive).IsRequired();
        
        builder.HasMany(u => u.HostConnections)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId);
        
        builder.HasMany(u => u.UrlConnections)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId);

    }
}