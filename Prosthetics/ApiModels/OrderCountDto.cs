namespace Prosthetics.ApiModels
{
    public class OrderCountDto
    {
        public required string OrderName { get; init; }
        public int Count { get; set; }
    }
}
