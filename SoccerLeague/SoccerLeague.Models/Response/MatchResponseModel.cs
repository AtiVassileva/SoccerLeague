using System.ComponentModel;

namespace SoccerLeague.Models.Response
{
    public class MatchResponseModel : BaseResponseModel
    {
        [DisplayName("League")]
        public string? LeagueName { get; set; }

        [DisplayName("League")]
        public Guid LeagueId { get; set; }

        [DisplayName("Arena")]
        public string? ArenaName { get; set; }

        [DisplayName("Arena")]
        public Guid ArenaId { get; set; }

        [DisplayName("Host")]
        public string? HostTeamName { get; set; }

        [DisplayName("Host")] 
        public Guid HostTeamId { get; set; }

        [DisplayName("Guest")]
        public string? GuestTeamName { get; set; }

        [DisplayName("Guest")]
        public Guid GuestTeamId { get; set; }

        [DisplayName("Match Date")]
        public DateTime PlayedOn { get; set; }

        public int HostGoals { get; set; }

        public int GuestGoals { get; set; }
    }
}