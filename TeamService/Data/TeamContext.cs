using MongoDB.Driver;
using TeamService.Models;

namespace TeamService.Data
{
    public class TeamContext 
    {
        private readonly IMongoDatabase _database;

        public TeamContext(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("TeamDB");
        }

        public IMongoCollection<Team> Teams => _database.GetCollection<Team>("Teams");
    }
}

