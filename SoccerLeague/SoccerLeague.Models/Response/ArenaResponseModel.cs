namespace SoccerLeague.Models.Response
{
    public class ArenaResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public Guid HomeTeamId { get; set; }
    }
}