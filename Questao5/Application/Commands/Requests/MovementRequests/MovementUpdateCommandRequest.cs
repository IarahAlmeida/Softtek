using MediatR;
using Questao5.Application.Commands.Responses.MovementResponses;

namespace Questao5.Application.Commands.Requests.MovementRequests
{
    public class MovementUpdateCommandRequest : IRequest<MovementUpdateCommandResponse>
    {
        public string Id { get; private set; }
        public string AccountId { get; private set; }
        public string Date { get; private set; }
        public char Type { get; private set; }
        public double Value { get; private set; }

        public MovementUpdateCommandRequest(string id, string accountId, string date, char type, double value)
        {
            Id = id;
            AccountId = accountId;
            Date = date;
            Type = type;
            Value = value;
        }
    }
}