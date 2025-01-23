using Homebroker.Application.DTO;
using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Interfaces;
using Homebroker.Infrastructure;

namespace Homebroker.Application
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _repository;

        public WalletService(IWalletRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(WalletInputDTO dto)
        {
            var wallet = new Wallet()
            {
                Name = dto.Name
            };

            await _repository.Create(wallet);
            await _repository.UnitOfWork.Commit();
        }

        public async Task<IEnumerable<Wallet>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<WalletAsset>> GetWalletAssetsByWalletId(Guid walletId)
        {
            return await _repository.GetWalletAssetsByWalletId(walletId);
        }
    }
}
