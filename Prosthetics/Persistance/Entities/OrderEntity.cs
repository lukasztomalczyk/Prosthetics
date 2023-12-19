using System.ComponentModel.DataAnnotations.Schema;

namespace Prosthetics.Persistance.Entities
{
    public class OrderEntity : BaseEntity
    {
        public string Type { get; set; }
        [NotMapped]
        public List<AdditionalWorkEntityOrderEntity> AdditionalWorkEntityOrders { get; } = new();
        public List<AdditionalWorkEntity> AdditionalWorks { get; set; } = new();
        public DateTime InsertedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string? Comments { get; set; }
        public int Status { get; set; }
        public int DoctorId { get; set; }
        public DoctorEntity Doctor { get; set; }
    }
}
