using JWTWithBCrypt.Model;
using JWTWithBCrypt.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace JWTWithBCrypt.Service
{
    public class MongoDbService
    {
        private readonly IMongoCollection<User> _users;
        public MongoDbService(IOptions<MongoDbSettings> opts)
        {
            var cfg = opts.Value;
            var client = new MongoClient(cfg.ConnectionString);
            var db = client.GetDatabase(cfg.DatabaseName);
            _users = db.GetCollection<User>(cfg.UsersCollection);
        }
        public Task<User> GetUserAsync(string username)
            => _users.Find(u => u.Username == username).FirstOrDefaultAsync();

        public Task AddUserAsync(User user)
            => _users.InsertOneAsync(user);
    }
}
