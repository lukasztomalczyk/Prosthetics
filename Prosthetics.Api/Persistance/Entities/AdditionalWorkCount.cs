namespace Prosthetics.Persistance.Entities
{
    public class AdditionalWorkCount : BaseEntity
    {
        public int Count { get; set; }
        public List<Order> Orders { get; set; } = new();
        public int AdditionalWorkId { get; set; }
        public AdditionalWork AdditionalWork { get; set; }
    }
}
