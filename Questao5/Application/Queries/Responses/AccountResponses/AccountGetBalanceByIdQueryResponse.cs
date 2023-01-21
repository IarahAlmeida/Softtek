using Questao5.Application.Base;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Queries.Responses.AccountResponses
{
    public class AccountGetBalanceByIdQueryResponse : BaseResponse
    {
        public long? Number { get; set; }

        public string? Name { get; set; }

        public DateTime? ResponseAt { get; set; }

        public double? Balance { get; set; }

        public AccountGetBalanceByIdQueryResponse(long number, string name, double balance) : base()
        {
            Number = number;
            Name = name;
            ResponseAt = DateTime.Now;
            Balance = balance;
        }

        public AccountGetBalanceByIdQueryResponse(ErrorType type = ErrorType.Internal, string errorMessage = "Something went wrong.") : base(type, errorMessage)
        {
            Number = null;
            Name = null;
            ResponseAt = null;
            Balance = null;
        }
    }
}