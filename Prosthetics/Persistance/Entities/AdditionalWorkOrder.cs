namespace Prosthetics.Persistance.Entities
{
    public class AdditionalWorkOrder : BaseEntity
    {
        public int OrdersId { get; set; }
        public int AdditionalWorksId { get; set; }
        public Order Order { get; set; } = null!;
        public AdditionalWork AdditionalWork { get; set; } = null!;
    }
}
