using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using P03PlayersWebAPI.Models;

namespace P03PlayersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolleyballPlayersController : ControllerBase
    {
        private readonly VolleyballWebContext _context;
 
        public VolleyballPlayersController(VolleyballWebContext context)
        {
            _context = context;
        }

        // GET: api/VolleyballPlayers

        // http://localhost:000/api/VolleyballPlayers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolleyballPlayer>>> GetVolleyballPlayers()
        {
          if (_context.VolleyballPlayers == null)
          {
              return NotFound();
          }
            return await _context.VolleyballPlayers.ToListAsync();
        }

        // GET: api/VolleyballPlayers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VolleyballPlayer>> GetVolleyballPlayer(int id)
        {
          if (_context.VolleyballPlayers == null)
          {
              return NotFound();
          }
            var volleyballPlayer = await _context.VolleyballPlayers.FindAsync(id);

            if (volleyballPlayer == null)
            {
                return NotFound();
            }

            return volleyballPlayer;
        }

        // PUT: api/VolleyballPlayers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVolleyballPlayer(int id, VolleyballPlayer volleyballPlayer)
        {
            if (id != volleyballPlayer.Id)
            {
                return BadRequest();
            }

            _context.Entry(volleyballPlayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolleyballPlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VolleyballPlayers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VolleyballPlayer>> PostVolleyballPlayer(VolleyballPlayer volleyballPlayer)
        {
          if (_context.VolleyballPlayers == null)
          {
              return Problem("Entity set 'VolleyballWebContext.VolleyballPlayers'  is null.");
          }
            _context.VolleyballPlayers.Add(volleyballPlayer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVolleyballPlayer", new { id = volleyballPlayer.Id }, volleyballPlayer);
        }

        // DELETE: api/VolleyballPlayers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolleyballPlayer(int id)
        {
            if (_context.VolleyballPlayers == null)
            {
                return NotFound();
            }
            var volleyballPlayer = await _context.VolleyballPlayers.FindAsync(id);
            if (volleyballPlayer == null)
            {
                return NotFound();
            }

            _context.VolleyballPlayers.Remove(volleyballPlayer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VolleyballPlayerExists(int id)
        {
            return (_context.VolleyballPlayers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
