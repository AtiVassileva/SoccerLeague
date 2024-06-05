using System.ComponentModel.DataAnnotations;

namespace SoccerLeague.Models.Request
{
    public class TeamRequestModel
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string Country { get; set; } = null!;

        [Required]
        public string AuthorId { get; set; } = null!;

        public string? LogoUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int Points { get; set; }

        [Required]
        public Guid LeagueId { get; set; }
    }
}