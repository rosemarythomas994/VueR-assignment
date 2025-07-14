using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Revv_Cars.Model;
using RevvCar.Models;

namespace Revv_Cars.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserRepository(IMongoClient mongoClient, IOptions<MongoDBSettings> mongoDBSettings)
        {
            var database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _usersCollection = database.GetCollection<User>(mongoDBSettings.Value.UsersCollection);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }
    }

}
