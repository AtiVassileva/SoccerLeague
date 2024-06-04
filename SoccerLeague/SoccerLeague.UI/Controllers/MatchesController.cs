using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Models.Data;

namespace SoccerLeague.UI.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Matches.Include(m => m.Arena).Include(m => m.Author).Include(m => m.Guest).Include(m => m.Host).Include(m => m.League);
            return View(await applicationDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Arena)
                .Include(m => m.Author)
                .Include(m => m.Guest)
                .Include(m => m.Host)
                .Include(m => m.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }
        
        public IActionResult Create()
        {
            ViewData["ArenaId"] = new SelectList(_context.Arenas, "Id", "AuthorId");
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            ViewData["GuestId"] = new SelectList(_context.Teams, "Id", "AuthorId");
            ViewData["HostId"] = new SelectList(_context.Teams, "Id", "AuthorId");
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "AuthorId");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeagueId,HostId,GuestId,ArenaId,PlayedOn,HostGoals,GuestGoals,Id,Name,Country,AuthorId")] Match match)
        {
            if (ModelState.IsValid)
            {
                match.Id = Guid.NewGuid();
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArenaId"] = new SelectList(_context.Arenas, "Id", "AuthorId", match.ArenaId);
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", match.AuthorId);
            ViewData["GuestId"] = new SelectList(_context.Teams, "Id", "AuthorId", match.GuestId);
            ViewData["HostId"] = new SelectList(_context.Teams, "Id", "AuthorId", match.HostId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "AuthorId", match.LeagueId);
            return View(match);
        }
        
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["ArenaId"] = new SelectList(_context.Arenas, "Id", "AuthorId", match.ArenaId);
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", match.AuthorId);
            ViewData["GuestId"] = new SelectList(_context.Teams, "Id", "AuthorId", match.GuestId);
            ViewData["HostId"] = new SelectList(_context.Teams, "Id", "AuthorId", match.HostId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "AuthorId", match.LeagueId);
            return View(match);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LeagueId,HostId,GuestId,ArenaId,PlayedOn,HostGoals,GuestGoals,Id,Name,Country,AuthorId")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            ViewData["ArenaId"] = new SelectList(_context.Arenas, "Id", "AuthorId", match.ArenaId);
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", match.AuthorId);
            ViewData["GuestId"] = new SelectList(_context.Teams, "Id", "AuthorId", match.GuestId);
            ViewData["HostId"] = new SelectList(_context.Teams, "Id", "AuthorId", match.HostId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "AuthorId", match.LeagueId);
            return View(match);
        }
        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Arena)
                .Include(m => m.Author)
                .Include(m => m.Guest)
                .Include(m => m.Host)
                .Include(m => m.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Matches'  is null.");
            }
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(Guid id)
        {
          return (_context.Matches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}