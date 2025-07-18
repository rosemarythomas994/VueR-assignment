using Revv_Cars.Model;

namespace Revv_Cars.Repository
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
