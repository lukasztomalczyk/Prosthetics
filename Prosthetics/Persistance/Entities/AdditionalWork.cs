using System.ComponentModel.DataAnnotations.Schema;

namespace Prosthetics.Persistance.Entities
{
    public class AdditionalWork : BaseEntity
    {
        public string Name { get; set; }
        [NotMapped]
        public List<AdditionalWorkOrder> AdditionalWorkEntityOrders { get; } = new();
        public List<Order> Orders { get; set; } = new();
    }
}
