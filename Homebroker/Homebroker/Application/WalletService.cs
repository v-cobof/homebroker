using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Interfaces;

namespace Homebroker.Application
{
    public class WalletService : GenericService<Wallet>, IWalletService
    {
        public WalletService(IRepository<Wallet> repository) : base(repository)
        {
        }


    }
}
