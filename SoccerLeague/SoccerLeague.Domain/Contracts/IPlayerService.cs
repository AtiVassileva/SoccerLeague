using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain.Contracts
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerResponseModel>> GetAll();
        Task<PlayerResponseModel> GetById(Guid id);
        Task<bool> Create(PlayerRequestModel model);
        Task<bool> Edit(Guid id, PlayerRequestModel model);
        Task<bool> Delete(Guid id);
    }
}