using Microsoft.AspNetCore.Identity;

namespace SoccerLeague.Models.Data
{
    public class User : IdentityUser
    {
        public IEnumerable<Arena> AddedArenas { get; set; } = new HashSet<Arena>();
        public IEnumerable<League> AddedLeagues { get; set; } = new HashSet<League>();
        public IEnumerable<Match> AddedMatches { get; set; } = new HashSet<Match>();
        public IEnumerable<Player> AddedPlayers { get; set; } = new HashSet<Player>();
        public IEnumerable<Team> AddedTeams { get; set; } = new HashSet<Team>();
    }
}