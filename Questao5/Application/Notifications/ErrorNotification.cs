using MediatR;

namespace Questao5.Application.Notifications
{
    public class ErrorNotification : INotification
    {
        public string Error { get; set; }
        public string StackTrace { get; set; }

        public ErrorNotification(string error, string stackTrace)
        {
            Error = error;
            StackTrace = stackTrace;
        }
    }
}