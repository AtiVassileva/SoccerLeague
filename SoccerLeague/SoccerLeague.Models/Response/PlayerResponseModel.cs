using System.ComponentModel;

namespace SoccerLeague.Models.Response
{
    public class PlayerResponseModel : BaseResponseModel
    {
        [DisplayName("Picture")]
        public string? PictureUrl { get; set; }

        [DisplayName("Team")]
        public string? TeamName { get; set; }

        [DisplayName("Team")]
        public Guid TeamId { get; set; }
    }
}