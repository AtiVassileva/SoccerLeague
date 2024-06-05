using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class LeagueRequestModel : BaseRequestModel
    {
        [Url]
        public string? LogoUrl { get; set; }
    }
}