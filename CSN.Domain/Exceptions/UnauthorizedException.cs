using CSN.Domain.Interfaces.Exception;

namespace CSN.Domain.Exceptions
{
    public class UnauthorizedException : Exception, IException
    {
        public int Status { get; } = 401;
        public string? ClientMessage { get; set; }
        public UnauthorizedException(string message, string clientMessage) : base(message)
        {
            this.ClientMessage = clientMessage;
        }
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
