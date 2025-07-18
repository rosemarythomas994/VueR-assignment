using Revv_car_CQRS.Model;

namespace Revv_car_CQRS.Repository
{
    public interface ICarRepository
    {
        List<Car> Get();
        Car Get(string id);
        Car Create(Car car);
        void Update(string id, Car carIn);
        void Remove(string id);
    }
}
