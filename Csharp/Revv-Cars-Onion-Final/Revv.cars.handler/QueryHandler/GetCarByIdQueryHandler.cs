using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Revv.cars.handler.RepoInterface;
using Revv.cars.Shared.Queries;

namespace Revv.cars.handler.QueryHandler
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQueryRequest, GetCarByIdQueryResponse>
    {
        private readonly ICarRepository _repo;

        public GetCarByIdQueryHandler(ICarRepository repo)
        {
            _repo = repo;
        }

        public async Task<GetCarByIdQueryResponse> Handle(GetCarByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var car = _repo.Get(request.Id);
            if (car == null) return null!;

            return await Task.FromResult(new GetCarByIdQueryResponse
            {
                Id = car.Id,
                Image = car.Image,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Place = car.Place,
                Number = car.Number,
                Date = car.Date,
                CreatedAt = car.CreatedAt,
                UpdatedAt = car.UpdatedAt,
                UserId = car.UserId
            });
        }
    }
}
