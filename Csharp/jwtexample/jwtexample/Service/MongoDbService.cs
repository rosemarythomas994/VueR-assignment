using jwtexample.Model;
using MongoDB.Driver;

namespace jwtexample.Service
{
    public class MongoDbService
    {
        private readonly IMongoCollection<User> _users;

        public MongoDbService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _users = database.GetCollection<User>(config["MongoDbSettings:UsersCollection"]);
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }
    }
}
