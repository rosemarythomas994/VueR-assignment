using Revv_Cars.Model;
using Revv_Cars.Repository;

namespace Revv_Cars.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public List<Car> Get() => _carRepository.Get();

        public Car Get(string id) => _carRepository.Get(id);

        public Car Create(Car car) => _carRepository.Create(car);

        public void Update(string id, Car car) => _carRepository.Update(id, car);

        public void Remove(string id) => _carRepository.Remove(id);
    }
}
