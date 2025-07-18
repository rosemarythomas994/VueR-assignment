using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Revv.cars.Shared.Queries
{
    public class GetCarByIdQueryRequest : IRequest<GetCarByIdQueryResponse>
    {
        public string Id { get; set; } = default!;
    }
}
