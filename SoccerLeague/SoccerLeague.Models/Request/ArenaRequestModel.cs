using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class ArenaRequestModel : BaseRequestModel
    {
        [Required]
        public Guid HomeTeamId { get; set; }
    }
}