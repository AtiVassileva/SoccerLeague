using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Data;
using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlayerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerResponseModel>> GetAll()
        {
            var players = await _context.Players
                .OrderBy(a => a.Name)
                .Include(p => p.Team)
                .ToListAsync();

            var playerResponseModels = _mapper.Map<IEnumerable<PlayerResponseModel>>(players);
            return playerResponseModels;
        }

        public async Task<PlayerResponseModel> GetById(Guid id)
        {
            var player = await FindPlayer(id);
            var playerResponseModel = _mapper.Map<PlayerResponseModel>(player);
            return playerResponseModel;
        }

        public async Task<bool> Create(PlayerRequestModel model)
        {
            var player = _mapper.Map<Player>(model);
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Guid id, PlayerRequestModel model)
        {
            var player = await FindPlayer(id);

            player!.Name = model.Name;
            player!.Country = model.Country;
            player!.PictureUrl = model.PictureUrl;
            player!.TeamId = model.TeamId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var player = await FindPlayer(id);
            _context.Players.Remove(player!);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<Player?> FindPlayer(Guid id)
        {
            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (player == null)
            {
                throw new NullReferenceException("Player does not exist!");
            }

            return player;
        }
    }
}