namespace Homebroker.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }
    }
}
