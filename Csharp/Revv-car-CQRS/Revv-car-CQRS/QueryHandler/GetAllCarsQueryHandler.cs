using MediatR;
using Revv_car_CQRS.Queries;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.QueryHandler
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQueryRequest, GetAllCarsQueryResponse>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<GetAllCarsQueryResponse> Handle(GetAllCarsQueryRequest request, CancellationToken cancellationToken)
        {
            var cars = await Task.FromResult(_carRepository.Get());
            return new GetAllCarsQueryResponse { Cars = cars };
        }
    }
}
