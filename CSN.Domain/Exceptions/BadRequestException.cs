using CSN.Domain.Interfaces.Exception;

namespace CSN.Domain.Exceptions
{
    public class BadRequestException : Exception, IException
    {
        public int Status { get; } = 400;
        public string? ClientMessage { get; set; }
        public BadRequestException(string message, string clientMessage) : base(message)
        {
            this.ClientMessage = clientMessage;
        }
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
