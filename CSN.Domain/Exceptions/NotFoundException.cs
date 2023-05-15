using CSN.Domain.Interfaces.Exception;

namespace CSN.Domain.Exceptions
{
    public class NotFoundException : Exception, IException
    {
        public int Status { get; } = 404;
        public string? ClientMessage { get; set; } 
        public NotFoundException(string message, string clientMessage) : base(message)
        {
            this.ClientMessage = clientMessage;
        }
        public NotFoundException(string message) : base(message)
        {
            this.ClientMessage = message;
        }
    }
}
