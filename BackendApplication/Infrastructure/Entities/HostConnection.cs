
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entities;

public class HostConnection : BaseConnection
{
    public int Port { get; set; }
    public string Host { get; set; }
    public string DatabaseOrSchema { get; set; }
}

public class HostConnectionConfiguration : BaseConnectionConfiguration<HostConnection>, IEntityTypeConfiguration<HostConnection>
{
    public void Configure(EntityTypeBuilder<HostConnection> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Port).IsRequired();
        
        builder.Property(e => e.Host).IsRequired().HasMaxLength(255);
        
        builder.Property(e => e.DatabaseOrSchema).IsRequired().HasMaxLength(255);

        builder.HasOne(e => e.User)
            .WithMany(u => u.HostConnections)
            .HasForeignKey(e => e.UserId);
    }
}