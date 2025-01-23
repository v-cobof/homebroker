using Homebroker.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Homebroker.Infrastructure.Configurations
{
    public class WalletAssetConfiguration : IEntityTypeConfiguration<WalletAsset>
    {
        public void Configure(EntityTypeBuilder<WalletAsset> builder)
        {
            builder.HasKey(wa => new { wa.WalletId, wa.AssetId });  // Definindo chave composta

            builder.Property(wa => wa.Shares)
                   .IsRequired();

            // Garantindo unicidade para WalletId e AssetId
            builder.HasIndex(wa => new { wa.WalletId, wa.AssetId })
                   .IsUnique();
        }
    }
}
