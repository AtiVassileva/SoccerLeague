using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Data.Models;

namespace SoccerLeague.UI.Controllers
{
    public class LeaguesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaguesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Leagues.Include(l => l.Author);
            return View(await applicationDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Leagues == null)
            {
                return NotFound();
            }

            var league = await _context.Leagues
                .Include(l => l.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }
        
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogoUrl,Id,Name,Country,AuthorId")] League league)
        {
            if (ModelState.IsValid)
            {
                league.Id = Guid.NewGuid();
                _context.Add(league);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", league.AuthorId);
            return View(league);
        }
        
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Leagues == null)
            {
                return NotFound();
            }

            var league = await _context.Leagues.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", league.AuthorId);
            return View(league);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LogoUrl,Id,Name,Country,AuthorId")] League league)
        {
            if (id != league.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(league);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueExists(league.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", league.AuthorId);
            return View(league);
        }
        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Leagues == null)
            {
                return NotFound();
            }

            var league = await _context.Leagues
                .Include(l => l.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Leagues == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Leagues'  is null.");
            }
            var league = await _context.Leagues.FindAsync(id);
            if (league != null)
            {
                _context.Leagues.Remove(league);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueExists(Guid id)
        {
          return (_context.Leagues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}