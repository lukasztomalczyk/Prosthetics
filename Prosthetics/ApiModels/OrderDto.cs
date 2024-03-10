namespace Prosthetics.ApiModels
{
    public class OrderDto
    {
        public int Id { get; set; }
        public required string PatientFullName { get; set; }
        public required DateTime OrderDate { get; set; }
        public required DateTime DeadLine { get; set; }
        public required string Type { get; set; }
        public List<AdditionalWorkCountDto> AdditionalWorksCounts { get; set; } = new List<AdditionalWorkCountDto>();
        public int AdditionalWorksCount { get; set; }
        public string? Comments { get; set; }
        public required string ShortComment { get; set; }
        public required string Status { get; set; }
        public int OrderStatusId { get; set; }
    }
}
