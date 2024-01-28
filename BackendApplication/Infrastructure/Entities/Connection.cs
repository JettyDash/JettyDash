
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;using Schemes.Enums;

namespace Infrastructure.Entities;

public class Connection
{
    public int Id { get; set; }
    public ConnectionType ConnectionType { get; set; }
    public int UserId { get; set; }
    public string DatabaseName { get; set; }
    public DatabaseType Type { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateTime { get; set; }
    
    public virtual User User { get; set; }
}

public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
    
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId).IsRequired();

        builder.Property(e => e.DatabaseName).IsRequired().HasMaxLength(255);

        builder.Property(e => e.Type).IsRequired().HasConversion<string>();

        builder.Property(e => e.CreationDate).IsRequired();

        builder.Property(e => e.LastUpdateTime).IsRequired();

    }

}



// public abstract class BaseConnectionConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseConnection
// {
//     public void Configure(EntityTypeBuilder<TBase> builder)
//     {
//     
//         builder.HasKey(e => e.Id);
//
//         builder.Property(e => e.UserId).IsRequired();
//
//         builder.Property(e => e.DatabaseName).IsRequired().HasMaxLength(255);
//
//         builder.Property(e => e.Type).IsRequired().HasConversion<string>();
//
//         builder.Property(e => e.CreationDate).IsRequired();
//
//         builder.Property(e => e.LastUpdateTime).IsRequired();
//
//     }
//
// }
// public class HostConnectionConfiguration : BaseConnectionConfiguration<HostConnection>, IEntityTypeConfiguration<HostConnection>
// {
//     public void Configure(EntityTypeBuilder<HostConnection> builder)
//     {
//         base.Configure(builder);
//         builder.Property(e => e.Port).IsRequired();
//         
//         builder.Property(e => e.Host).IsRequired().HasMaxLength(255);
//         
//         builder.Property(e => e.DatabaseOrSchema).IsRequired().HasMaxLength(255);
//
//         builder.HasOne(e => e.User)
//             .WithMany(u => u.HostConnections)
//             .HasForeignKey(e => e.UserId);
//     }
// }