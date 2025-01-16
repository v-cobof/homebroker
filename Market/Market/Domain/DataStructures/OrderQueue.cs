using Market.Domain.Entities;

namespace Market.Domain.DataStructures
{
    public class OrderQueue
    {
        private List<Order> _orders;

        private Queue<Order> _queue;

        //public IReadOnlyCollection<Order> Orders { get => _orders; }

        public OrderQueue()
        {
            _orders = new List<Order>();
        }

        public bool Less(int i, int j)
        {
            return _orders[i].Price < _orders[j].Price;
        }

        public void Swap(int i, int j)
        {
            (_orders[j], _orders[i]) = (_orders[i], _orders[j]);
        }

        public int Len()
        {
            return _orders.Count;
        }

        public void Push(Order order)
        {
            _orders.Add(order);
        }

        public Order Pop()
        {
            var item = _orders.First();
            _orders.Remove(item);
            return item;
        }

        public Order Peek()
        {
            return _orders.First();
        }
    }
}
