using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using Revv.cars.Shared.Queries;
using Revv.cars.Domain.Model;
using Revv.cars.handler.RepoInterface;


namespace Revv.Car.handler.Handler
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
