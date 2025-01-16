namespace Market.Domain.Entities
{
    public class Asset
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int MarketVolume { get; private set; }

        public Asset(string id, string name, int volume) : base()
        {
            Id = id;
            Name = name;
            MarketVolume = volume;
        }
    }
}
