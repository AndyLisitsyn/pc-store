using System;

namespace NonDomain
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public int RecordsAffected { get; set; }
        public string Message { get; set; }
        public object OperationId { get; set; }

        public string ExceptionMessage => Exception?.Message;
        public string ExceptionStackTrace => Exception?.StackTrace;
        public string ExceptionInnerMessage => Exception?.InnerException?.Message;
        public string ExceptionInnerStackTrace => Exception?.InnerException?.StackTrace;

        public Exception Exception { get; set; }

        public string FormattedExceptionMessages => $"Base exception: {ExceptionMessage} -> Inner exception: {ExceptionInnerMessage}";

        public static OperationResult CreateFromException(string message, Exception ex)
        {
            var status = new OperationResult
            {
                IsSuccess = false,
                Message = message,
                OperationId = null
            };

            if (ex != null)
            {
                status.Exception = ex;
            }

            return status;
        }
    }
}
