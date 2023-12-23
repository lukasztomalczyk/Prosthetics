namespace Prosthetics.Features
{
    public class Store : IStore
    {
        public OrderStore Order { get; set; } = new OrderStore();
    }

    public interface IStore
    {
        OrderStore Order { get; set; }
    }

    public class OrderStore
    {
        public int DoctorId { get; set; }
        public string DoctorFullName { get; set; }
        public int OrderId { get; set; }
    }
}
