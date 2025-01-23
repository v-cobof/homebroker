using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Interfaces;

namespace Homebroker.Application
{
    public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet> _repository;

        public WalletService(IRepository<Wallet> repository)
        {
            _repository = repository;
        }

        public async Task Create(Wallet asset)
        {
            await _repository.Create(asset);
        }

        public async Task<IEnumerable<Wallet>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
    }
}
