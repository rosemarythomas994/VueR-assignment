using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Revv.cars.Domain.Model;
using Revv.cars.DomainService.RepoInterface;

namespace Revv.cars.Application.Repository.repository
{
    public class CarRepository : ICarRepository
    {
        private readonly IMongoCollection<Car> _cars;

        public CarRepository(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>("cars");
        }

        public List<Car> Get()
        {
            var filter = Builders<Car>.Filter.Empty;
            return _cars.Find(filter).ToList();
        }

        public Car Get(string id)
        {
            var filter = Builders<Car>.Filter.Eq("Id", id);
            return _cars.Find(filter).FirstOrDefault();
        }

        public Car Create(Car car)
        {
            _cars.InsertOne(car);
            return car;
        }

        public void Update(string id, Car carIn)
        {
            var filter = Builders<Car>.Filter.Eq("Id", id);
            var result = _cars.ReplaceOne(filter, carIn);
            if (result.MatchedCount == 0)
                throw new KeyNotFoundException($"No car found with ID {id}");
        }

        public void Remove(string id)
        {
            var filter = Builders<Car>.Filter.Eq("Id", id);
            var result = _cars.DeleteOne(filter);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException($"No car found to delete with ID {id}");
        }
    }
}
