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

        public ArenaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArenaResponseModel>> GetAll()
        {
            var arenaResponseModels = await _context.Arenas
                .Select(a => new ArenaResponseModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Country = a.Country,
                    HomeTeamId = a.HomeTeamId
                })
                .ToListAsync();

            return arenaResponseModels;
        }

        public async Task<ArenaResponseModel> GetById(Guid id)
        {
            var arena = await FindArena(id);

            var arenaResponseModel = new ArenaResponseModel
            {
                Name = arena!.Name,
                Country = arena!.Country,
                HomeTeamId = arena!.HomeTeamId
            };

            return arenaResponseModel;
        }

        public async Task<bool> Create(ArenaRequestModel model)
        {
            var arena = new Arena
            {
                Name = model.Name,
                Country = model.Country,
                AuthorId = model.AuthorId,
                HomeTeamId = model.HomeTeamId
            };

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