using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schemes.Enum;

namespace Infrastructure.Entity;

public class Connection
{
    public int ConnectionId { get; set; }
    // public string VaultIdentifier { get; set; }
    public ConnectionType ConnectionType { get; set; }
    public int UserId { get; set; }
    public string DatabaseName { get; set; }
    public DatabaseType DatabaseType { get; set; }
    public ConnectionStatus Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateTime { get; set; }

    public virtual User User { get; set; }
}

public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder.HasKey(e => e.ConnectionId);
        
        builder.Property(e => e.UserId).IsRequired();
        
        // builder.Property(e => e.VaultIdentifier).IsRequired().HasMaxLength(255);

        builder.Property(e => e.ConnectionType).IsRequired().HasConversion<string>();

        builder.Property(e => e.DatabaseName).IsRequired().HasMaxLength(255);

        builder.Property(e => e.DatabaseType).IsRequired().HasConversion<string>();
        
        builder.Property(e => e.Status).IsRequired().HasConversion<string>();

        builder.Property(e => e.CreationDate).IsRequired();

        builder.Property(e => e.LastUpdateTime).IsRequired();

        builder.HasOne(u => u.User)
            .WithMany(u => u.Connections)
            .HasForeignKey(u => u.UserId);
    }
}