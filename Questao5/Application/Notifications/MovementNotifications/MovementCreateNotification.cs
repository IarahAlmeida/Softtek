using MediatR;

namespace Questao5.Application.Notifications.MovementNotifications
{
    public class MovementCreateNotification : INotification
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Date { get; set; }

        public char Type { get; set; }

        public double Value { get; set; }

        public bool IsCreated { get; set; }

        public MovementCreateNotification(string id, string accountId, string date, char type, double value, bool isCreated)
        {
            Id = id;
            AccountId = accountId;
            Date = date;
            Type = type;
            Value = value;
            IsCreated = isCreated;
        }
    }
}