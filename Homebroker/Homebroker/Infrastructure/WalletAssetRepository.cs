using Homebroker.Application.DTO;
using Homebroker.Domain;
using Homebroker.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Homebroker.Infrastructure
{
    public class WalletAssetRepository : GenericRepository<WalletAsset>, IWalletAssetRepository
    {
        public WalletAssetRepository(IMongoClient mongoClient) : base(mongoClient)
        {
        }

        public async Task<IEnumerable<WalletAssetOutputDTO>> GetWalletAssetsByWalletId(string walletId)
        {
            var pipeline = new[]
            {
                new BsonDocument
                {
                    { "$lookup", new BsonDocument
                        {
                            { "from", "Asset" },         // Coleção com os detalhes dos ativos
                            { "localField", "AssetId" },  // Campo na coleção walletAssets
                            { "foreignField", "_id" },    // Campo correspondente na coleção assets
                            { "as", "Asset" }            // Nome do campo no resultado
                        }
                    }
                },
                new BsonDocument
                {
                    { "$unwind", "$Asset" }  // Garante que 'Asset' será um objeto único, não uma lista
                },
                new BsonDocument
                {
                    { "$project", new BsonDocument
                        {
                            { "_id", 0 },
                            { "AssetId", 1 },
                            { "WalletId", 1 },
                            { "Shares", 1 },
                            { "Asset", 1 }  // Inclui os assets como lista
                        }
                    }
                }
            };

            var result = await _collection
                .Aggregate<WalletAssetOutputDTO>(pipeline)
                .ToListAsync();

            return result;
        }
    }
}
