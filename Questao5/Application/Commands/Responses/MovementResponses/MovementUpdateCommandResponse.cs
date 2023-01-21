using Questao5.Application.Base;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses.MovementResponses
{
    public class MovementUpdateCommandResponse : BaseResponse
    {
        public string? Id { get; set; } = null;

        public MovementUpdateCommandResponse(string id) : base()
        {
            Id = id;
        }

        public MovementUpdateCommandResponse(ErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
            Id = null;
        }
    }
}
