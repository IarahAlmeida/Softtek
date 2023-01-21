using MediatR;
using Questao5.Application.Commands.Responses.MovementResponses;

namespace Questao5.Application.Commands.Requests.MovementRequests
{
    public class MovementCreateCommandRequest : IRequest<MovementCreateCommandResponse>
    {
        public string AccountId { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public char Type { get; set; }
        public double Value { get; set; }
        public string IdempotencyId { get; set; } = string.Empty;

        public MovementCreateCommandRequest()
        {
        }

        public MovementCreateCommandRequest(string accountId, string date, char type, double value, string idempotencyId)
        {
            AccountId = accountId;
            Date = date;
            Type = type;
            Value = value;
            IdempotencyId = idempotencyId;
        }
    }
}