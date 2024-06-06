using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Request;

namespace SoccerLeague.UI.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;

        public PlayersController(IPlayerService playerService, ITeamService teamService)
        {
            _playerService = playerService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var playerData = await _playerService.GetAll();
            return View(playerData);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var player = await _playerService.GetById(id);
                return View(player);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Create()
        {
            var teams = await _teamService.GetAll();
            ViewBag.Teams = teams
                .Select(t => new KeyValuePair<Guid, string>(t.Id, t.Name))
                .ToDictionary(t => t.Key, t => t.Value);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _playerService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var teams = await _teamService.GetAll();
                ViewBag.Teams = teams
                    .Select(t => new KeyValuePair<Guid, string>(t.Id, t.Name))
                    .ToDictionary(t => t.Key, t => t.Value);

                var player = await _playerService.GetById(id);
                return View(player);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, PlayerRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _playerService.Edit(id, model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var player = await _playerService.GetById(id);
            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _playerService.Delete(id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}