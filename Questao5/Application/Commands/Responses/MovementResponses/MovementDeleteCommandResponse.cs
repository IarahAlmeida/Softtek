using Questao5.Application.Base;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses.MovementResponses
{
    public class MovementDeleteCommandResponse : BaseResponse
    {
        public string? Id { get; set; } = null;

        public MovementDeleteCommandResponse(string id) : base()
        {
            Id = id;
        }

        public MovementDeleteCommandResponse(ErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
            Id = null;
        }
    }
}