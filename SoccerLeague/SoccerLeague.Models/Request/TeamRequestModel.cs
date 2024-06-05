using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class TeamRequestModel : BaseRequestModel
    {
        [Url]
        public string? LogoUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int Points { get; set; }

        [Required]
        public Guid LeagueId { get; set; }
    }
}