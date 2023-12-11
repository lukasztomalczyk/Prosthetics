namespace Prosthetics.Persistance.Entities
{
    public class Order : Entity
    {
        public string Type { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public int Status { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
