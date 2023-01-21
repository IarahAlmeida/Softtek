using MediatR;
using Questao5.Application.Notifications;
using Questao5.Application.Notifications.MovementNotifications;

namespace Questao5.Application.Handlers
{
    public class LogEventHandler : INotificationHandler<MovementCreateNotification>,
                                   INotificationHandler<ErrorNotification>
    {
        public Task Handle(MovementCreateNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CREATE: '{notification.Id} - {notification.AccountId} - {notification.Date} - {notification.Type} - {notification.Value} - {notification.IsCreated}'");
            }, cancellationToken);
        }

        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERROR: '{notification.Error} \n {notification.StackTrace}'");
            }, cancellationToken);
        }
    }
}