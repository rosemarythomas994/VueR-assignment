using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Revv_car_CQRS.Model;

namespace Revv_car_CQRS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _users = database.GetCollection<User>("users");
        }

        public User? GetByUsername(string username) =>
            _users.Find(u => u.Username == username).FirstOrDefault();

        public User? GetByUsernameAndPassword(string username, string password)
        {
            var user = GetByUsername(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                return user;

            return null;
        }

        public void Create(User user)
        {
            _users.InsertOne(user);
        }
    }

}
