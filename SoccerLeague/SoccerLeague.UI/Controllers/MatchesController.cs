using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Request;
using SoccerLeague.UI.Infrastructure;

namespace SoccerLeague.UI.Controllers
{
    [Authorize]
    public class MatchesController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ILeagueService _leagueService;
        private readonly IArenaService _arenaService;
        private readonly ITeamService _teamService;

        public MatchesController(IMatchService matchService, ILeagueService leagueService, IArenaService arenaService, ITeamService teamService)
        {
            _matchService = matchService;
            _leagueService = leagueService;
            _arenaService = arenaService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var matchData = await _matchService.GetAll();
            return View(matchData);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var match = await _matchService.GetById(id);
                return View(match);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Create()
        {
            await GetSelectDropdownsData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatchRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _matchService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var match = await _matchService.GetById(id);

                if (match.AuthorId != User.GetId())
                {
                    return Unauthorized();
                }

                await GetSelectDropdownsData();
                return View(match);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, MatchRequestModel model)
        {
            if (model.AuthorId != User.GetId())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _matchService.Edit(id, model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var match = await _matchService.GetById(id);

            if (match.AuthorId != User.GetId())
            {
                return Unauthorized();
            }

            return View(match);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _matchService.Delete(id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        private async Task GetSelectDropdownsData()
        {
            var leagues = await _leagueService.GetAll();
            ViewBag.Leagues = leagues
                .Select(t => new KeyValuePair<Guid, string>(t.Id, t.Name))
                .ToDictionary(t => t.Key, t => t.Value);

            var arenas = await _arenaService.GetAll();
            ViewBag.Arenas = arenas
                .Select(t => new KeyValuePair<Guid, string>(t.Id, t.Name))
                .ToDictionary(t => t.Key, t => t.Value);

            var teams = await _teamService.GetAll();
            ViewBag.Teams = teams
                .Select(t => new KeyValuePair<Guid, string>(t.Id, t.Name))
                .ToDictionary(t => t.Key, t => t.Value);
        }
    }
}