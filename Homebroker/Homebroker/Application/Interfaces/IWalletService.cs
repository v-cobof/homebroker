using Homebroker.Domain;

namespace Homebroker.Application.Interfaces
{
    public interface IWalletService
    {
        public Task Create(Wallet asset);
        public Task<IEnumerable<Wallet>> GetAll();
    }
}
