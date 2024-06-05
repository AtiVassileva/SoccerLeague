using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class PlayerRequestModel : BaseRequestModel
    {
        [Url]
        [DisplayName("Picture")]
        public string? PictureUrl { get; set; }

        [Required]
        [DisplayName("Team")]
        public Guid TeamId { get; set; }
    }
}