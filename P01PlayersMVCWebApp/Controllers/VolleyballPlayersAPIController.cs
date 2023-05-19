using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P01PlayersMVCWebApp.Models;

namespace P01PlayersMVCWebApp.Controllers
{
    public class VolleyballPlayersAPIController : Controller
    {
        private readonly VolleyballWebContext _context;

        public VolleyballPlayersAPIController(VolleyballWebContext context)
        {
            _context = context;
        }

        // GET: VolleyballPlayersAPI
        public async Task<IActionResult> Index()
        {
              return _context.VolleyballPlayers != null ? 
                          View(await _context.VolleyballPlayers.ToListAsync()) :
                          Problem("Entity set 'VolleyballWebContext.VolleyballPlayers'  is null.");
        }

        // GET: VolleyballPlayersAPI/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VolleyballPlayers == null)
            {
                return NotFound();
            }

            var volleyballPlayer = await _context.VolleyballPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volleyballPlayer == null)
            {
                return NotFound();
            }

            return View(volleyballPlayer);
        }

        // GET: VolleyballPlayersAPI/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VolleyballPlayersAPI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Position,Number")] VolleyballPlayer volleyballPlayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volleyballPlayer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volleyballPlayer);
        }

        // GET: VolleyballPlayersAPI/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VolleyballPlayers == null)
            {
                return NotFound();
            }

            var volleyballPlayer = await _context.VolleyballPlayers.FindAsync(id);
            if (volleyballPlayer == null)
            {
                return NotFound();
            }
            return View(volleyballPlayer);
        }

        // POST: VolleyballPlayersAPI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Position,Number")] VolleyballPlayer volleyballPlayer)
        {
            if (id != volleyballPlayer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volleyballPlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolleyballPlayerExists(volleyballPlayer.Id))
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
            return View(volleyballPlayer);
        }

        // GET: VolleyballPlayersAPI/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VolleyballPlayers == null)
            {
                return NotFound();
            }

            var volleyballPlayer = await _context.VolleyballPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volleyballPlayer == null)
            {
                return NotFound();
            }

            return View(volleyballPlayer);
        }

        // POST: VolleyballPlayersAPI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VolleyballPlayers == null)
            {
                return Problem("Entity set 'VolleyballWebContext.VolleyballPlayers'  is null.");
            }
            var volleyballPlayer = await _context.VolleyballPlayers.FindAsync(id);
            if (volleyballPlayer != null)
            {
                _context.VolleyballPlayers.Remove(volleyballPlayer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolleyballPlayerExists(int id)
        {
          return (_context.VolleyballPlayers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
