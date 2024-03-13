namespace Prosthetics.Api.Persistance.Entities
{
    public class Patient : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
