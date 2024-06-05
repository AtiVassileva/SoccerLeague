using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain.Contracts
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueResponseModel>> GetAll();
        Task<LeagueResponseModel> GetById(Guid id);
        Task<bool> Create(LeagueRequestModel model);
        Task<bool> Edit(Guid id, LeagueRequestModel model);
        Task<bool> Delete(Guid id);
    }
}