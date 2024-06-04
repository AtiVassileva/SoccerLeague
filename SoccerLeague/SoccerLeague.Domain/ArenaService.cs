using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Data;
using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain
{
    public class ArenaService : IArenaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ArenaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArenaResponseModel>> GetAll()
        {
            var arenas = await _context.Arenas.ToListAsync();
            var arenaResponseModels = _mapper.Map<IEnumerable<ArenaResponseModel>>(arenas);
            return arenaResponseModels;
        }

        public async Task<ArenaResponseModel> GetById(Guid id)
        {
            var arena = await FindArena(id);
            var arenaResponseModel = _mapper.Map<ArenaResponseModel>(arena);
            return arenaResponseModel;
        }

        public async Task<bool> Create(ArenaRequestModel model)
        {
            var arena = _mapper.Map<Arena>(model);
            await _context.Arenas.AddAsync(arena);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Guid id, ArenaRequestModel model)
        {
            var arena = await FindArena(id);

            arena!.Name = model.Name;
            arena!.Country = model.Country;
            arena!.HomeTeamId = model.HomeTeamId;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var arena = await FindArena(id);
            _context.Arenas.Remove(arena!);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<Arena?> FindArena(Guid id)
        {
            var arena = await _context.Arenas.FirstOrDefaultAsync(a => a.Id == id);

            if (arena == null)
            {
                throw new NullReferenceException("Arena does not exist!");
            }

            return arena;
        }
    }
}