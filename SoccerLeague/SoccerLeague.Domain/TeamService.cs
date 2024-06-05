using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Data;
using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.Domain
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeamService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamResponseModel>> GetAll()
        {
            var teams = await _context.Teams
                .Include(t => t.League)
                .ToListAsync();

            var teamResponseModels = _mapper.Map<IEnumerable<TeamResponseModel>>(teams);
            return teamResponseModels;
        }

        public async Task<TeamResponseModel> GetById(Guid id)
        {
            var team = await FindTeam(id);
            var teamResponseModel = _mapper.Map<TeamResponseModel>(team);
            return teamResponseModel;
        }

        public async Task<bool> Create(TeamRequestModel model)
        {
            var team = _mapper.Map<Team>(model);
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Guid id, TeamRequestModel model)
        {
            var team = await FindTeam(id);

            team!.Name = model.Name;
            team!.Country = model.Country;
            team!.LogoUrl = model.LogoUrl;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var team = await FindTeam(id);
            _context.Teams.Remove(team!);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<Team?> FindTeam(Guid id)
        {
            var team = await _context.Teams
                .Include(t => t.League)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (team == null)
            {
                throw new NullReferenceException("Team does not exist!");
            }

            return team;
        }
    }
}
