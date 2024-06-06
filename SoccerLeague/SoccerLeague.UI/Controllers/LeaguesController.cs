using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Request;

namespace SoccerLeague.UI.Controllers
{
    [Authorize]
    public class LeaguesController : Controller
    {
        private readonly ILeagueService _leagueService;
        private readonly ApplicationDbContext _context;

        public LeaguesController(ILeagueService leagueService, ApplicationDbContext context)
        {
            _leagueService = leagueService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var leagueData = await _leagueService.GetAll();
            return View(leagueData);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var league = await _leagueService.GetById(id);
                return View(league);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Teams = await _context
                .Teams
                .Select(t => new KeyValuePair<Guid, string>(t.Id, t.Name))
                .ToDictionaryAsync(t => t.Key, t => t.Value);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _leagueService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var league = await _leagueService.GetById(id);
                return View(league);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, LeagueRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _leagueService.Edit(id, model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var league = await _leagueService.GetById(id);
            return View(league);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _leagueService.Delete(id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}