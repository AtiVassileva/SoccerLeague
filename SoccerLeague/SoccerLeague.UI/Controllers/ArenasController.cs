using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Domain.Contracts;
using SoccerLeague.Models.Request;
using SoccerLeague.UI.Infrastructure;

namespace SoccerLeague.UI.Controllers
{
    public class ArenasController : Controller
    {
        private readonly IArenaService _arenaService;
        private readonly ApplicationDbContext _context;

        public ArenasController(IArenaService arenaService, ApplicationDbContext context)
        {
            _arenaService = arenaService;
            _context = context;
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
            ViewBag.Teams = await _context
                .Teams
                .Select(t => new KeyValuePair<Guid,string>(t.Id, t.Name))
                .ToDictionaryAsync(t => t.Key, t => t.Value);
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