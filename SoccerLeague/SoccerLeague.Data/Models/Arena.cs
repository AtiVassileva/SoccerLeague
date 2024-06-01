using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Data.Models
{
    public class Arena : BaseEntity
    {
        [Required]
        public Guid HomeTeamId { get; set; }

        public Team? HomeTeam { get; set; }
    }
}