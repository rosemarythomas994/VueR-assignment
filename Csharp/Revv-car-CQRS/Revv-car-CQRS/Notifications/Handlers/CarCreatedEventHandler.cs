using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Revv_car_CQRS.Notifications.Handlers
{
    public class CarCreatedEventHandler : INotificationHandler<CarCreatedEvent>
    {
        private readonly ILogger<CarCreatedEventHandler> _logger;
        public CarCreatedEventHandler(ILogger<CarCreatedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(CarCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [EVENT FIRED] New car created:");
            _logger.LogInformation("Car ID: {CarId}, Brand: {Brand}, Model: {Model}, Year: {Year}, Place: {Place}, UserId: {UserId}",
                notification.CarId,
                notification.Brand,
                notification.Model,
                notification.Year,
                notification.Place,
                notification.UserId);
            Console.WriteLine("🚘 [EVENT FIRED] A new car has been created:");
            Console.WriteLine($"   ID     : {notification.CarId}");
            Console.WriteLine($"   Brand  : {notification.Brand}");
            Console.WriteLine($"   Model  : {notification.Model}");
            Console.WriteLine($"   Year   : {notification.Year}");
            Console.WriteLine($"   Place  : {notification.Place}");
            Console.WriteLine($"   UserId : {notification.UserId}");

            return Task.CompletedTask;
        }
    }
}
