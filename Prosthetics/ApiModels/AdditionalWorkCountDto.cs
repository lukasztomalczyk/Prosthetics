namespace Prosthetics.ApiModels
{
    public class AdditionalWorkCountDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Count { get; set; } = "0";
    }
}
