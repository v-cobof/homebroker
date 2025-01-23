using Homebroker.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Homebroker.Infrastructure.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Symbol)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasMany(a => a.WalletAssets)
                   .WithOne(wa => wa.Asset)
                   .HasForeignKey(wa => wa.AssetId);

            builder.HasMany(a => a.Orders)
                .WithOne(o => o.Asset)
                .HasForeignKey(o => o.AssetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
