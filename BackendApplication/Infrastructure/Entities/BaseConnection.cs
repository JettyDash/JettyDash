
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;using Schemes.Enums;

namespace Infrastructure.Entities;

public abstract class BaseConnection
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string DatabaseName { get; set; }
    public DatabaseType Type { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateTime { get; set; }
    
    public virtual User User { get; set; }
}

public abstract class BaseConnectionConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseConnection
{
    public void Configure(EntityTypeBuilder<TBase> builder)
    {
    
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId).IsRequired();

        builder.Property(e => e.DatabaseName).IsRequired().HasMaxLength(255);

        builder.Property(e => e.Type).IsRequired().HasConversion<string>();

        builder.Property(e => e.Username).IsRequired().HasMaxLength(255);

        builder.Property(e => e.Password).IsRequired().HasMaxLength(255);

        builder.Property(e => e.CreationDate).IsRequired();

        builder.Property(e => e.LastUpdateTime).IsRequired();

    }

}
        