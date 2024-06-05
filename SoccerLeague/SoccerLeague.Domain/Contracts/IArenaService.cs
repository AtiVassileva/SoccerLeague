using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain.Contracts
{
    public interface IArenaService
    {
        Task<IEnumerable<ArenaResponseModel>> GetAll();
        Task<ArenaResponseModel> GetById(Guid id);
        Task<bool> Create(ArenaRequestModel model);
        Task<bool> Edit(Guid id, ArenaRequestModel model);
        Task<bool> Delete(Guid id);
    }
}