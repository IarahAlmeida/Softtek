using MediatR;
using Questao5.Application.Commands.Responses.MovementResponses;

namespace Questao5.Application.Commands.Requests.MovementRequests
{
    public class MovementDeleteCommandRequest : IRequest<MovementDeleteCommandResponse>
    {
        public string Id { get; set; }

        public MovementDeleteCommandRequest(string id)
        {
            Id = id;
        }
    }
}