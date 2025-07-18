using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Revv.cars.Domain.Model;


namespace Revv.cars.handler.RepoInterface
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
