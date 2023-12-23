namespace Prosthetics.Features
{
    public class Store : IStore
    {
        public OrderStore Order { get; set; } = new OrderStore();
        public PatientStore Patient { get; set; } = new PatientStore();
    }

    public interface IStore
    {
        OrderStore Order { get; set; }
        PatientStore Patient { get; set; }
    }

    public class OrderStore
    {
        public int DoctorId { get; set; }
        public string DoctorFullName { get; set; }
        public int OrderId { get; set; }
    }

    public class PatientStore
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
