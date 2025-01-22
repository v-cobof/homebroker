using Homebroker.Domain;
using System.Text.Json.Serialization;

namespace Homebroker.Application.DTO
{
    public record WalletAssetOutputDTO
    {
        public string AssetId { get; set; }
        public int Shares { get; set; }
        public string WalletId { get; set; }

        public Asset Asset { get; set; }
    }

    public record WalletAssetInputDTO
    {
        public string AssetId { get; set;}
        public int Shares { get; set;}
    }
}
