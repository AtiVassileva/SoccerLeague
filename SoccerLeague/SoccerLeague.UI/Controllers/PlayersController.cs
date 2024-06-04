using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Models.Data;

namespace SoccerLeague.UI.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Players.Include(p => p.Author).Include(p => p.Team);
            return View(await applicationDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Author)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }
        
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "AuthorId");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,PictureUrl,Id,Name,Country,AuthorId")] Player player)
        {
            if (ModelState.IsValid)
            {
                player.Id = Guid.NewGuid();
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", player.AuthorId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "AuthorId", player.TeamId);
            return View(player);
        }
        
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", player.AuthorId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "AuthorId", player.TeamId);
            return View(player);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TeamId,PictureUrl,Id,Name,Country,AuthorId")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id", player.AuthorId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "AuthorId", player.TeamId);
            return View(player);
        }
        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Author)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Players == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Players'  is null.");
            }
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(Guid id)
        {
          return (_context.Players?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}