using CSN.Domain.Interfaces.Exception;

namespace CSN.Domain.Exceptions
{
    public class ForbiddenException : Exception, IException
    {
        public int Status { get; } = 403;
        public string? ClientMessage { get; set; }
        public ForbiddenException(string message, string clientMessage) : base(message)
        {
            this.ClientMessage = clientMessage;
        }
        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
