using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamService.Models;
using TeamService.Repositories;

namespace TeamService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamRepository _repository;

        public TeamController(TeamRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = await _repository.GetAllTeams();
            return Ok(teams);
        }

        [HttpGet("{id:length(24)}", Name = "GetTeam")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _repository.GetTeam(id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            await _repository.CreateTeam(team);

            return CreatedAtRoute("GetTeam", new { id = team.Id }, team);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateTeam(int id, Team teamIn)
        {
            var team = await _repository.GetTeam(id);

            if (team == null)
            {
                return NotFound();
            }

            await _repository.UpdateTeam(id, teamIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _repository.GetTeam(id);

            if (team == null)
            {
                return NotFound();
            }

            await _repository.RemoveTeam(id);

            return NoContent();
        }
    }
}
