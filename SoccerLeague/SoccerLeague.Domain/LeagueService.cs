using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Data;
using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain
{
    public class LeagueService : ILeagueService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LeagueService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeagueResponseModel>> GetAll()
        {
            var leagues = await _context.Leagues
                .ToListAsync();

            var leagueResponseModels = _mapper.Map<IEnumerable<LeagueResponseModel>>(leagues);
            return leagueResponseModels;
        }

        public async Task<LeagueResponseModel> GetById(Guid id)
        {
            var league = await FindLeague(id);
            var leagueResponseModel = _mapper.Map<LeagueResponseModel>(league);
            return leagueResponseModel;
        }

        public async Task<bool> Create(LeagueRequestModel model)
        {
            var league = _mapper.Map<League>(model);
            await _context.Leagues.AddAsync(league);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Guid id, LeagueRequestModel model)
        {
            var league = await FindLeague(id);

            league!.Name = model.Name;
            league!.Country = model.Country;
            league!.LogoUrl = model.LogoUrl;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var league = await FindLeague(id);
            _context.Leagues.Remove(league!);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<League?> FindLeague(Guid id)
        {
            var league = await _context.Leagues
                .FirstOrDefaultAsync(a => a.Id == id);

            if (league == null)
            {
                throw new NullReferenceException("League does not exist!");
            }

            return league;
        }
    }
}