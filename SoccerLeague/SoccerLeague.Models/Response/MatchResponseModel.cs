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
        public string? HostName { get; set; }

        [DisplayName("Host")] 
        public Guid HostId { get; set; }

        [DisplayName("Guest")]
        public string? GuestName { get; set; }

        [DisplayName("Guest")]
        public Guid GuestId { get; set; }

        [DisplayName("Match Date")]
        public DateTime PlayedOn { get; set; }

        public int HostGoals { get; set; }

        public int GuestGoals { get; set; }
    }
}