namespace Prosthetics.ApiModels
{
    public class DoctorOrdersByPatientDto
    {
        public required string DoctorFullName { get; init; }
        public IEnumerable<PatientOrdersDto> OrdersByPatients { get; set; } = new List<PatientOrdersDto>();
        public List<OrderCountDto> Summary { get; set; } = new List<OrderCountDto>();
    }
}
