using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using Revv.cars.handler.RepoInterface;
using Revv.cars.Shared.Commands;

namespace Revv.cars.handler.CommandHandler
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommandRequest, DeleteCarCommandResponse>
    {
        private readonly ICarRepository _repo;

        public DeleteCarCommandHandler(ICarRepository repo)
        {
            _repo = repo;
        }

        public Task<DeleteCarCommandResponse> Handle(DeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            var result = new DeleteCarCommandResponse();

            try
            {
                var car = _repo.Get(request.Id);
                if (car == null)
                {
                    result.IsSuccess = false;
                    result.Message = $"Car with ID {request.Id} not found.";
                    return Task.FromResult(result);
                }

                _repo.Remove(request.Id);
                result.IsSuccess = true;
                result.Message = "Car deleted successfully.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
            }

            return Task.FromResult(result);
        }
    }
}
