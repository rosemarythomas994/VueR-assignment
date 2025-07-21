// File: Notifications/Handlers/CarCreatedEventHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Revv_car_CQRS.Notifications.Handlers
{
    public class CarCreatedEventHandler : INotificationHandler<CarCreatedEvent>
    {
        public Task Handle(CarCreatedEvent notification, CancellationToken cancellationToken)
        {
            // You could log to file, DB, or call external API here
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
