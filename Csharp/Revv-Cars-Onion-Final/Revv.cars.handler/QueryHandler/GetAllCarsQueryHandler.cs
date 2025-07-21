using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Revv.cars.Domain.Model;
using Revv.cars.handler.RepoInterface;
using Revv.cars.Shared;
using Revv.cars.Shared.Queries;


namespace Revv.cars.handler.QueryHandler
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQueryRequest, GetAllCarsQueryResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetAllCarsQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<GetAllCarsQueryResponse> Handle(GetAllCarsQueryRequest request, CancellationToken cancellationToken)
        {
            var domainCars = await Task.FromResult(_carRepository.Get());
            var sharedCars = _mapper.Map<List<Revv.cars.Shared.Car>>(domainCars);

            return new GetAllCarsQueryResponse { Cars = sharedCars };
        }
    }
}
