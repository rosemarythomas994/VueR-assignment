using Microsoft.Extensions.Logging;
using NLog;
using Revv_Cars.Model;
using Revv_Cars.Repository;
namespace Revv_Cars.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CarService> _logger;

        public CarService(ICarRepository carRepository, ILogger<CarService> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }


        public List<Car> Get()
        {
            _logger.LogInformation("Fetching all cars");
            return _carRepository.Get();
        }
        public Car Get(string id)
        {
            _logger.LogInformation("Fetching car with ID: {Id}", id);
            return _carRepository.Get(id);
        }
        public Car Create(Car car)
        {
            _logger.LogInformation("Creating a new car entry");
            return _carRepository.Create(car);
        }
        public void Update(string id, Car car)
        {
            _logger.LogWarning("Updating car with ID: {Id}", id);
            _carRepository.Update(id, car);
        }
        public void Remove(string id)
        {
            _logger.LogError("Deleting car with ID: {Id}", id);
            _carRepository.Remove(id);
        }
    }
}
