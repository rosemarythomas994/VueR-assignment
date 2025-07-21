using MediatR;
using Revv_car_CQRS.Queries;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.QueryHandler
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
