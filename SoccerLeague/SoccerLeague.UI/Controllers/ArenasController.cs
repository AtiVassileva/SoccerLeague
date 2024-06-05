using Microsoft.AspNetCore.Mvc;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Request;

namespace SoccerLeague.UI.Controllers
{
    public class ArenasController : Controller
    {
        private readonly IArenaService _arenaService;
        private readonly ITeamService _teamService;

        public ArenasController(IArenaService arenaService, ITeamService teamService)
        {
            _arenaService = arenaService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var arenaData = await _arenaService.GetAll();
            return View(arenaData);
        }
        
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var arena = await _arenaService.GetById(id);
                return View(arena);
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
                .Select(t => new KeyValuePair<Guid,string>(t.Id, t.Name))
                .ToDictionary(t => t.Key, t => t.Value);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArenaRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _arenaService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var arena = await _arenaService.GetById(id);
                return View(arena);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, ArenaRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _arenaService.Edit(id, model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {
            var arena = await _arenaService.GetById(id);
            return View(arena);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _arenaService.Delete(id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}