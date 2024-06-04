using SoccerLeague.Models.Data;
using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain.Contracts
{
    public interface IArenaService
    {
        IEnumerable<Arena> GetAll();
        IEnumerable<ArenaResponseModel> GetById(Guid id);
        bool Create(ArenaRequestModel model);
        bool Edit(Guid id, ArenaRequestModel model);
        bool Delete(Guid id);
    }
}