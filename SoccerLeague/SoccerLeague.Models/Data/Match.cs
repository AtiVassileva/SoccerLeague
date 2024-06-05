using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Data
{
    public class Match : BaseEntity
    {
        [Required]
        public Guid LeagueId { get; set; }

        public League League { get; set; } = null!;

        [Required]
        public Guid HostId { get; set; }
        public Team Host { get; set; } = null!;

        [Required]
        public Guid GuestId { get; set; }
        public Team Guest { get; set; } = null!;

        [Required]
        public Guid ArenaId { get; set; }
        public Arena Arena { get; set; } = null!;

        public DateTime PlayedOn { get; set; }
        public int HostGoals { get; set; }
        public int GuestGoals { get; set; }
    }
}