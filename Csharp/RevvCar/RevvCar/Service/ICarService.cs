using RevvCar.Models;

namespace RevvCar.Services
{
    public interface ICarService
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(string id);
        Task CreateCarAsync(Car car);
        Task UpdateCarAsync(string id, Car car);
        Task DeleteCarAsync(string id);
    }
}