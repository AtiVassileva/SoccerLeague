using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class PlayerRequestModel : BaseRequestModel
    {
        [Url]
        public string? PictureUrl { get; set; }

        [Required]
        public Guid TeamId { get; set; }
    }
}