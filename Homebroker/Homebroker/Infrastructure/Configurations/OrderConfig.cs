using Homebroker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Homebroker.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Definição da chave primária
            builder.HasKey(o => o.Id);

            // Definição de propriedades obrigatórias e tamanho máximo
            builder.Property(o => o.WalletId)
                .IsRequired();

            builder.Property(o => o.AssetId)
                .IsRequired();

            builder.Property(o => o.Shares)
                .IsRequired();

            builder.Property(o => o.Price)
                .IsRequired();

            builder.Property(o => o.Type)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired();

            builder.Property(o => o.Partial)
                .IsRequired();

            // Índice para melhorar consultas por WalletId
            builder.HasIndex(o => o.WalletId);

            builder.HasOne(o => o.Wallet)
                .WithMany(w => w.Orders)
                .HasForeignKey(o => o.WalletId)
                .OnDelete(DeleteBehavior.Restrict); // Evita remoção em cascata

            builder.HasOne(o => o.Asset)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AssetId)
                .OnDelete(DeleteBehavior.Restrict); // Evita remoção em cascata

            builder.HasMany(o => o.Transactions)
                .WithOne(t => t.Order)
                .HasForeignKey(t => t.OrderId);
        }
    }
}
