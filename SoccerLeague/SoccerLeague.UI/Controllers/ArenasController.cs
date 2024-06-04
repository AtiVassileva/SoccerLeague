using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Models.Data;

namespace SoccerLeague.UI.Controllers
{
    public class ArenasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArenasController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Arenas.Include(a => a.Author).Include(a => a.HomeTeam);
            return View(await applicationDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Arenas == null)
            {
                return NotFound();
            }

            var arena = await _context.Arenas
                .Include(a => a.Author)
                .Include(a => a.HomeTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arena == null)
            {
                return NotFound();
            }

            return View(arena);
        }
        
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "AuthorId");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeTeamId,Id,Name,Country,AuthorId")] Arena arena)
        {
            if (ModelState.IsValid)
            {
                arena.Id = Guid.NewGuid();
                _context.Add(arena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", arena.AuthorId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "AuthorId", arena.HomeTeamId);
            return View(arena);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Arenas == null)
            {
                return NotFound();
            }

            var arena = await _context.Arenas.FindAsync(id);
            if (arena == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", arena.AuthorId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "AuthorId", arena.HomeTeamId);
            return View(arena);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("HomeTeamId,Id,Name,Country,AuthorId")] Arena arena)
        {
            if (id != arena.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArenaExists(arena.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", arena.AuthorId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "AuthorId", arena.HomeTeamId);
            return View(arena);
        }
        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Arenas == null)
            {
                return NotFound();
            }

            var arena = await _context.Arenas
                .Include(a => a.Author)
                .Include(a => a.HomeTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arena == null)
            {
                return NotFound();
            }

            return View(arena);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Arenas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Arenas'  is null.");
            }
            var arena = await _context.Arenas.FindAsync(id);
            if (arena != null)
            {
                _context.Arenas.Remove(arena);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArenaExists(Guid id)
        {
          return (_context.Arenas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}