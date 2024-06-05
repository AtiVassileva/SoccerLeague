using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Data
{
    public class Player : BaseEntity
    {
        [Required]
        public Guid TeamId { get; set; }
        public Team Team { get; set; } = null!;
        public string? PictureUrl { get; set; }
    }
}