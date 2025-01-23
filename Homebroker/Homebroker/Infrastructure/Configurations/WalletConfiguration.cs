using Homebroker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Homebroker.Infrastructure.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(w => w.WalletAssets)
                   .WithOne(wa => wa.Wallet)
                   .HasForeignKey(wa => wa.WalletId);

            builder.HasMany(w => w.Orders)
                .WithOne(o => o.Wallet)
                .HasForeignKey(o => o.WalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
