using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Data.Models
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string Country { get; set; } = null!;
    }
}