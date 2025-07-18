using MediatR;
using Revv_car_CQRS.Model;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.Queries
{
    public class GetAllCarsQueryResponse
    {
        public List<Car> Cars { get; set; } = new();
    }
}
