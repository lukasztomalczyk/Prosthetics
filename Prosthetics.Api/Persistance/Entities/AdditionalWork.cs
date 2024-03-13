namespace Prosthetics.Api.Persistance.Entities
{
    public class AdditionalWork : BaseEntity
    {
        public required string Name { get; set; }
        public List<AdditionalWorkCount> AdditionalWorkCounts { get; set; } = new();
    }
}
