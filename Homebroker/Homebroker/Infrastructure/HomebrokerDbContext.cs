using Homebroker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Homebroker.Infrastructure
{
    public class HomebrokerDbContext : DbContext
    {
        public HomebrokerDbContext(DbContextOptions<HomebrokerDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletAsset> WalletAssets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomebrokerDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
