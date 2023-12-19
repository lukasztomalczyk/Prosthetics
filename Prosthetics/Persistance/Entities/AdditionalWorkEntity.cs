using System.ComponentModel.DataAnnotations.Schema;

namespace Prosthetics.Persistance.Entities
{
    public class AdditionalWorkEntity : BaseEntity
    {
        public string Name { get; set; }
        [NotMapped]
        public List<AdditionalWorkEntityOrderEntity> AdditionalWorkEntityOrders { get; } = new();
        public List<OrderEntity> Orders { get; set; } = new();
    }
}
