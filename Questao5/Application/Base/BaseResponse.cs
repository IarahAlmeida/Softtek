using Questao5.Domain.Enumerators;

namespace Questao5.Application.Base
{
    public abstract class BaseResponse
    {
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }

        public BaseResponse()
        {
            ErrorCode = null;
            ErrorMessage = null;
        }

        public BaseResponse(ErrorType errorType, string errorMessage = "Something went wrong.")
        {
            ErrorCode = errorType.ToString();
            ErrorMessage = errorMessage;
        }
    }
}