using Revv_Cars.Model;

namespace Revv_Cars.Service
{
    public interface ICarService
    {
        List<Car> Get();
        Car Get(string id);
        Car Create(Car car);
        void Update(string id, Car car);
        void Remove(string id);
    }
}
