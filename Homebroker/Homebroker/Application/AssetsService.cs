using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Homebroker.Domain.Interfaces;

namespace Homebroker.Application
{
    public class AssetsService : GenericService<Asset>, IAssetsService
    {
        public AssetsService(IRepository<Asset> repository) : base(repository)
        {
        }
    }
}
