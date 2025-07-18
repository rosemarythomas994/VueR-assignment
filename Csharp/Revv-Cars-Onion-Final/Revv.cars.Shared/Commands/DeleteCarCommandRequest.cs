using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Revv.cars.Shared.Commands
{
    public class DeleteCarCommandRequest : IRequest<DeleteCarCommandResponse>
    {
        public string Id { get; set; } = default!;
    }
}
