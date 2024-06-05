using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class LeagueRequestModel : BaseRequestModel
    {
        [Url]
        [DisplayName("Logo")]
        public string? LogoUrl { get; set; }
    }
}