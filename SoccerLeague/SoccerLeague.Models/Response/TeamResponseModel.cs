using System.ComponentModel;

namespace SoccerLeague.Models.Response
{
    public class TeamResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public int Points { get; set; }
        [DisplayName("League")]
        public string? LeagueName { get; set; }
        public Guid LeagueId { get; set; }
    }
}