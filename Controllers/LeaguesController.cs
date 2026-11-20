
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sep2026.FootballWorld.Data;

namespace Sep2026.FootballWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly FootballDbContext _context;

        public LeaguesController(FootballDbContext context)
        {
            _context = context;
        }

        // GET: api/Leagues
        [HttpGet]
        public async Task<IActionResult> GetLeagues()
        {
            var leagues = await _context.Leagues.ToListAsync();

            return Ok(leagues);
        }

        // GET: api/Leagues/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeague(int id)
        {
            var league = await _context.Leagues
                .FirstOrDefaultAsync(l => l.Id == id);

            if (league == null)
            {
                return NotFound("League not found.");
            }

            return Ok(league);
        }

        // POST: api/Leagues
        [HttpPost]
        public async Task<IActionResult> CreateLeague(League league)
        {
            league.Id = 0;

            _context.Leagues.Add(league);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetLeague),
                new { id = league.Id },
                league
            );
        }

        // PUT: api/Leagues/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeague(
            int id,
            League league)
        {
            if (id != league.Id)
            {
                return BadRequest("League ID mismatch.");
            }

            var existingLeague = await _context.Leagues
                .FindAsync(id);

            if (existingLeague == null)
            {
                return NotFound("League not found.");
            }

            existingLeague.Name = league.Name;
            existingLeague.Country = league.Country;

            await _context.SaveChangesAsync();

            return Ok(existingLeague);
        }

        // DELETE: api/Leagues/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(int id)
        {
            var league = await _context.Leagues.FindAsync(id);

            if (league == null)
            {
                return NotFound("League not found.");
            }

            _context.Leagues.Remove(league);

            await _context.SaveChangesAsync();

            return Ok("League deleted successfully.");
        }
    }
}
