using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entities;

public class UrlConnection : BaseConnection
{
    public string Url { get; set; }

}

public class UrlConnectionConfiguration : BaseConnectionConfiguration<UrlConnection>, IEntityTypeConfiguration<UrlConnection>
{
    public void Configure(EntityTypeBuilder<UrlConnection> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Url).IsRequired();

        builder.HasOne(e => e.User)
            .WithMany(u => u.UrlConnections)
            .HasForeignKey(e => e.UserId);
    }
}