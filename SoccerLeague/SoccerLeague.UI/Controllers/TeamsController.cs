using Microsoft.AspNetCore.Mvc;
using SoccerLeague.Data;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Request;

namespace SoccerLeague.UI.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly ILeagueService _leagueService;
        private readonly ApplicationDbContext _context;

        public TeamsController(ITeamService teamService, ApplicationDbContext context, ILeagueService leagueService)
        {
            _teamService = teamService;
            _context = context;
            _leagueService = leagueService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var teamData = await _teamService.GetAll();
            return View(teamData);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var team = await _teamService.GetById(id);
                return View(team);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Create()
        {
            var leagues = await _leagueService.GetAll();
            ViewBag.Leagues = leagues
                .Select(t => new KeyValuePair<Guid, string>(t.Id, t.Name))
                .ToDictionary(t => t.Key, t => t.Value);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _teamService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var team = await _teamService.GetById(id);
                return View(team);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, TeamRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _teamService.Edit(id, model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var team = await _teamService.GetById(id);
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _teamService.Delete(id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}