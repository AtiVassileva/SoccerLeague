using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class ArenaRequestModel : BaseRequestModel
    {
        [Required]
        [DisplayName("Home Team")]
        public Guid HomeTeamId { get; set; }
    }
}