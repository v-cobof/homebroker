
using Homebroker.Application;
using Homebroker.Application.Interfaces;
using Homebroker.Domain.Interfaces;
using Homebroker.Infrastructure;
using MongoDB.Driver;

namespace Homebroker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");

            builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnectionString));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IWalletAssetRepository, WalletAssetRepository>();

            builder.Services.AddScoped<IAssetsService, AssetsService>();
            builder.Services.AddScoped<IWalletService, WalletService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IWalletAssetService, WalletAssetService>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
