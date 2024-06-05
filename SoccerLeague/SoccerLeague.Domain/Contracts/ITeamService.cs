using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamResponseModel>> GetAll();
        Task<TeamResponseModel> GetById(Guid id);
        Task<bool> Create(TeamRequestModel model);
        Task<bool> Edit(Guid id, TeamRequestModel model);
        Task<bool> Delete(Guid id);
    }
}