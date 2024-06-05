using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Data
{
    public class Team : BaseEntity
    {
        [Required]
        public Guid LeagueId { get; set; }

        public virtual League League { get; set; } = null!;

        public int Points { get; set; }

        public string? LogoUrl { get; set; }

        public virtual IEnumerable<Match> HomeMatches { get; set; } = new HashSet<Match>();
        public virtual IEnumerable<Match> AwayMatches { get; set; } = new HashSet<Match>();
    }
}