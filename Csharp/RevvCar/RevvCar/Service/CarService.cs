using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Revv_Cars.Repository;
using RevvCar.Models;

namespace RevvCar.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepo;

        public CarService(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carRepo.GetAllCarsAsync();
        }

        public async Task<Car> GetCarByIdAsync(string id)
        {
            return await _carRepo.GetCarByIdAsync(id);
        }

        public async Task CreateCarAsync(Car car)
        {
            await _carRepo.CreateCarAsync(car);
        }

        public async Task UpdateCarAsync(string id, Car car)
        {
            await _carRepo.UpdateCarAsync(id, car);
        }

        public async Task DeleteCarAsync(string id)
        {
            await _carRepo.DeleteCarAsync(id);
        }
    }
}