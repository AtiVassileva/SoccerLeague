using Microsoft.AspNetCore.Identity;

namespace SoccerLeague.Models.Data
{
    public class User : IdentityUser
    {
        public virtual IEnumerable<Arena> AddedArenas { get; set; } = new HashSet<Arena>();
        public virtual IEnumerable<League> AddedLeagues { get; set; } = new HashSet<League>();
        public virtual IEnumerable<Match> AddedMatches { get; set; } = new HashSet<Match>();
        public virtual IEnumerable<Player> AddedPlayers { get; set; } = new HashSet<Player>();
        public virtual IEnumerable<Team> AddedTeams { get; set; } = new HashSet<Team>();
    }
}