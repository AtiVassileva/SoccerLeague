using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Data;
using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MatchResponseModel>> GetAll()
        {
            var matches = await _context.Matches
                .OrderByDescending(m => m.PlayedOn)
                .Include(m => m.Arena)
                .Include(m => m.League)
                .Include(m => m.Host)
                .Include(m => m.Guest)
                .ToListAsync();

            var matchResponseModels = _mapper.Map<IEnumerable<MatchResponseModel>>(matches);
            return matchResponseModels;
        }

        public async Task<MatchResponseModel> GetById(Guid id)
        {
            var match = await FindMatch(id);
            var matchResponseModel = _mapper.Map<MatchResponseModel>(match);
            return matchResponseModel;
        }

        public async Task<bool> Create(MatchRequestModel model)
        {
            var match = _mapper.Map<Match>(model);
            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Guid id, MatchRequestModel model)
        {
            var match = await FindMatch(id);

            match!.Name = model.Name;
            match!.Country = model.Country;
            match!.ArenaId = model.ArenaId;
            match!.HostId = model.HostId;
            match!.GuestId = model.GuestId;
            match!.PlayedOn = model.PlayedOn;
            match!.HostGoals = model.HostGoals;
            match!.GuestGoals = model.GuestGoals;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var match = await FindMatch(id);
            _context.Matches.Remove(match!);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<Match?> FindMatch(Guid id)
        {
            var match = await _context.Matches
                .Include(m => m.Arena)
                .Include(m => m.League)
                .Include(m => m.Host)
                .Include(m => m.Guest)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (match == null)
            {
                throw new NullReferenceException("Match does not exist!");
            }

            return match;
        }
    }
}