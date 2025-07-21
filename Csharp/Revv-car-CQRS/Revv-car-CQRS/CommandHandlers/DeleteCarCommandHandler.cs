using MediatR;
using Revv_car_CQRS.Commands;
using Revv_car_CQRS.Repository;

namespace Revv_car_CQRS.CommandHandlers
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
