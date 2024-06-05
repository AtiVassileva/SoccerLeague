using System.ComponentModel;

namespace SoccerLeague.Models.Response
{
    public class TeamResponseModel : BaseResponseModel
    {
        public string? LogoUrl { get; set; }
        public int Points { get; set; }

        [DisplayName("League")]
        public string? LeagueName { get; set; }
        public Guid LeagueId { get; set; }
    }
}