using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Homebroker.Domain;

namespace Homebroker.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Definição da chave primária
            builder.HasKey(t => t.Id);

            // Definição de propriedades obrigatórias e tamanho máximo
            builder.Property(t => t.OrderId)
                .IsRequired();

            builder.Property(t => t.InvestorId)
                .IsRequired();

            builder.Property(t => t.BrokerTransactionId)
                .IsRequired();

            builder.Property(t => t.Shares)
                .IsRequired();

            builder.Property(t => t.Price)
                .IsRequired();

            // Índice para melhorar consultas por OrderId
            builder.HasIndex(t => t.OrderId);

            builder.HasOne(t => t.Order)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.OrderId);
        }
    }
}
