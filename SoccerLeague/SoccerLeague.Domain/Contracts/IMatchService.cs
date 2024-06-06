using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain.Contracts
{
    public interface IMatchService
    {
        Task<IEnumerable<MatchResponseModel>> GetAll();
        Task<MatchResponseModel> GetById(Guid id);
        Task<bool> Create(MatchRequestModel model);
        Task<bool> Edit(Guid id, MatchRequestModel model);
        Task<bool> Delete(Guid id);
    }
}