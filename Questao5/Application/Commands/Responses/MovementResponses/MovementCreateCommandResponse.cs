using Newtonsoft.Json;
using Questao5.Application.Base;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses.MovementResponses
{
    [Serializable]
    public class MovementCreateCommandResponse : BaseResponse
    {
        public string? Id { get; set; } = null;

        [JsonConstructor]
        public MovementCreateCommandResponse(string id) : base()
        {
            Id = id;
        }

        public MovementCreateCommandResponse(ErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
            Id = null;
        }
    }
}