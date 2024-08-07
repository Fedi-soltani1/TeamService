using MongoDB.Driver;
using TeamService.Data;
using TeamService.Models;

namespace TeamService.Repositories
{
    public class TeamRepository
    {
        private readonly IMongoCollection<Team> _teams;

        public TeamRepository(TeamContext context)
        {
            _teams = context.Teams;
        }

        public async Task<List<Team>> GetAllTeams() => await _teams.Find(team => true).ToListAsync();

        public async Task<Team> GetTeam(int id) => await _teams.Find<Team>(team => team.Id == id).FirstOrDefaultAsync();

        public async Task CreateTeam(Team team) => await _teams.InsertOneAsync(team);

        public async Task UpdateTeam(int id, Team teamIn) => await _teams.ReplaceOneAsync(team => team.Id == id, teamIn);

        public async Task RemoveTeam(int id) => await _teams.DeleteOneAsync(team => team.Id == id);
    }
}
