using System.ComponentModel;

namespace SoccerLeague.Models.Response
{
    public class LeagueResponseModel : BaseResponseModel
    {
        [DisplayName("Logo")]
        public string? LogoUrl { get; set; }
    }
}