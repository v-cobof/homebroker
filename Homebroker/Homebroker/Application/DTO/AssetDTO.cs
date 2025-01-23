namespace Homebroker.Application.DTO
{
    public class AssetInputDTO
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
    }

    public class AssetResultDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
    }
}
