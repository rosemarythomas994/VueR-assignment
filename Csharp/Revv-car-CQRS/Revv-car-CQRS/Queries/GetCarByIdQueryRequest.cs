using MediatR;
using Revv_car_CQRS.Model;

namespace Revv_car_CQRS.Queries
{
    public class GetCarByIdQueryRequest : IRequest<GetCarByIdQueryResponse>
    {
        public string Id { get; set; } = default!;
    }
}
