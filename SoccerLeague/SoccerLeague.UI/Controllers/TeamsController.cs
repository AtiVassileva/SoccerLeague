using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Models.Data;

namespace SoccerLeague.UI.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teams.Include(t => t.Author).Include(t => t.League);
            return View(await applicationDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Author)
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
        
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "AuthorId");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeagueId,Points,LogoUrl,Id,Name,Country,AuthorId")] Team team)
        {
            if (ModelState.IsValid)
            {
                team.Id = Guid.NewGuid();
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", team.AuthorId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "AuthorId", team.LeagueId);
            return View(team);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", team.AuthorId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "AuthorId", team.LeagueId);
            return View(team);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LeagueId,Points,LogoUrl,Id,Name,Country,AuthorId")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", team.AuthorId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "AuthorId", team.LeagueId);
            return View(team);
        }
        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Author)
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Teams == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Teams'  is null.");
            }
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(Guid id)
        {
          return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}