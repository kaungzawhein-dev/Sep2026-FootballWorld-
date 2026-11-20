
using Sep2026.FootballWorld.Data;
using FootballWorld.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly FootballDbContext _context;
        private readonly FootballService _service;

        public TeamsController(
            FootballDbContext context,
            FootballService service)
        {
            _context = context;
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll(
            int? leagueId = null,
            int pageNo = 1,
            int pageSize = 10,
            string? category = null)
        {
            if (leagueId.HasValue)
            {
                var result = await _service.GetTeamsByLeague(
                    leagueId.Value,
                    pageNo,
                    pageSize);

                return Ok(result);
            }

            var data = await _service.GetAllTeams(
                pageNo,
                pageSize,
                category);

            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetTeamById(id);

            if (data == null)
            {
                return NotFound("Team not found.");
            }

            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Team model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Id = 0;
            model.League = null;

            bool leagueExists = await _context.Leagues
                .AnyAsync(x => x.Id == model.LeagueId);

            if (!leagueExists)
            {
                return BadRequest("Invalid LeagueId.");
            }

            _context.Teams.Add(model);

            await _context.SaveChangesAsync();

            var result = await _service.GetTeamById(model.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.Id },
                result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Team model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound("Team not found.");
            }

            bool leagueExists = await _context.Leagues
                .AnyAsync(x => x.Id == model.LeagueId);

            if (!leagueExists)
            {
                return BadRequest("Invalid LeagueId.");
            }

            team.LeagueId = model.LeagueId;
            team.Name = model.Name;
            team.Country = model.Country;
            team.FoundedYear = model.FoundedYear;
            team.Stadium = model.Stadium;
            team.Manager = model.Manager;
            team.LogoUrl = model.LogoUrl;
            team.Description = model.Description;

            await _context.SaveChangesAsync();

            var result = await _service.GetTeamById(id);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound("Team not found.");
            }

            _context.Teams.Remove(team);

            await _context.SaveChangesAsync();

            return Ok("Team deleted successfully.");
        }
    }
}
