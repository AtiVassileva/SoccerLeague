using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class MatchRequestModel : BaseRequestModel
    {
        [Required]
        [DisplayName("League")]
        public Guid LeagueId { get; set; }

        [Required]
        [DisplayName("Host")]
        public Guid HostId { get; set; }

        [Required]
        [DisplayName("Guest")]
        public Guid GuestId { get; set; }

        [Required]
        [DisplayName("Arena")]
        public Guid ArenaId { get; set; }

        [DisplayName("Match Date")]
        public DateTime PlayedOn { get; set; }

        [Range(0, int.MaxValue)]
        [DisplayName("Host Goals")]
        public int HostGoals { get; set; }

        [Range(0, int.MaxValue)]
        [DisplayName("Guest Goals")]
        public int GuestGoals { get; set; }
    }
}