namespace Prosthetics.Persistance.Entities
{
    public class AdditionalWorkEntityOrderEntity : BaseEntity
    {
        public int OrderId { get; set; }
        public int AdditionalWorkId { get; set; }
        public OrderEntity Order { get; set; } = null!;
        public AdditionalWorkEntity AdditionalWork { get; set; } = null!;
    }
}
