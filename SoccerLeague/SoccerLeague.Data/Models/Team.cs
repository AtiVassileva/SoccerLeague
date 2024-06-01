using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Data.Models
{
    public class Team : BaseEntity
    {
        [Required]
        public Guid LeagueId { get; set; }

        public League? League { get; set; }

        public int Points { get; set; }

        public string? LogoUrl { get; set; }

        public IEnumerable<Match> HomeMatches { get; set; } = new HashSet<Match>();
        public IEnumerable<Match> AwayMatches { get; set; } = new HashSet<Match>();
    }
}