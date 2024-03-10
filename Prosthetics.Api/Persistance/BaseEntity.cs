using System.ComponentModel.DataAnnotations;

namespace Prosthetics.Persistance
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
