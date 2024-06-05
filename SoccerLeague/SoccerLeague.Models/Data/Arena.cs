using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Data
{
    public class Arena : BaseEntity
    {
        [Required]
        public Guid HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; } = null!;
    }
}