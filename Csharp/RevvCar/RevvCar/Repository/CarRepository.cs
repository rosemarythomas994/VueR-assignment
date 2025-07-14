using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Revv_Cars.Model;
using Revv_Cars.Repository;
using RevvCar.Models;

namespace Revv_Cars.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly IMongoCollection<Car> _carsCollection;

        public CarRepository(IMongoClient mongoClient, IOptions<MongoDBSettings> mongoDBSettings)
        {
            var database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _carsCollection = database.GetCollection<Car>(mongoDBSettings.Value.CarsCollection);
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carsCollection.Find(car => true).ToListAsync();
        }

        public async Task<Car> GetCarByIdAsync(string id)
        {
            return await _carsCollection.Find(car => car._id == id).FirstOrDefaultAsync();
        }

        public async Task CreateCarAsync(Car car)
        {
            car.CreatedAt = DateTime.UtcNow.ToString("o");
            car.UpdatedAt = car.CreatedAt;
            await _carsCollection.InsertOneAsync(car);
        }

        public async Task UpdateCarAsync(string id, Car car)
        {
            car._id = id;
            car.UpdatedAt = DateTime.UtcNow.ToString("o");
            await _carsCollection.ReplaceOneAsync(c => c._id == id, car);
        }

        public async Task DeleteCarAsync(string id)
        {
            await _carsCollection.DeleteOneAsync(car => car._id == id);
        }
    }
}
