using System.ComponentModel;

namespace SoccerLeague.Models.Response
{
    public class PlayerResponseModel : BaseResponseModel
    {
        public string? PictureUrl { get; set; }

        [DisplayName("Team")]
        public string? TeamName { get; set; }
        public Guid TeamId { get; set; }
    }
}