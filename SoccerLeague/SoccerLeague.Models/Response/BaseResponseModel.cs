namespace SoccerLeague.Models.Response
{
    public class BaseResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
    }
}