namespace Prosthetics.ApiModels
{
    public class PatientOrdersDto
    {
        public required string PatientFullName { get; init; }
        public IEnumerable<OrderCountDto> Orders { get; set; } = new List<OrderCountDto>();
    }
}
