using System.ComponentModel;

namespace SoccerLeague.Models.Response
{
    public class TeamResponseModel : BaseResponseModel
    {
        [DisplayName("Logo")]
        public string? LogoUrl { get; set; }
        public int Points { get; set; }

        [DisplayName("League")]
        public string? LeagueName { get; set; }

        [DisplayName("League")]
        public Guid LeagueId { get; set; }
    }
}