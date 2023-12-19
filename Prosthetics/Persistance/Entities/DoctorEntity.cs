namespace Prosthetics.Persistance.Entities
{
    public class DoctorEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<OrderEntity> Orders { get; set; }
    }
}
