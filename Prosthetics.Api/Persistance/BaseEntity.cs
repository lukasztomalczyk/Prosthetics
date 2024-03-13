using System.ComponentModel.DataAnnotations;

namespace Prosthetics.Api.Persistance
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
