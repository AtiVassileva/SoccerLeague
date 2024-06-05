using System.ComponentModel;

namespace SoccerLeague.Models.Response
{
    public class ArenaResponseModel : BaseResponseModel
    {
        public Guid HomeTeamId { get; set; }

        [DisplayName("Home Team")]
        public string? HomeTeamName { get; set; }
    }
}