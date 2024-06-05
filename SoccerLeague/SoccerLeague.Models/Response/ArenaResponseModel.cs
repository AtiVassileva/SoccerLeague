using System.ComponentModel;

namespace SoccerLeague.Models.Response
{
    public class ArenaResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public Guid HomeTeamId { get; set; }
        [DisplayName("Home Team")]
        public string? HomeTeamName { get; set; }
        public string AuthorId { get; set; } = null!;
    }
}