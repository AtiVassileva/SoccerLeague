using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Data.Models
{
    public class Player : BaseEntity
    {
        [Required]
        public Guid TeamId { get; set; }
        public Team? Team { get; set; }
        public string? PictureUrl { get; set; }
    }
}