using Revv_Cars.Model;
using RevvCar.Models;

namespace Revv_Cars.Repository
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(string id);
        Task CreateCarAsync(Car car);
        Task UpdateCarAsync(string id, Car car);
        Task DeleteCarAsync(string id);
    }
}
