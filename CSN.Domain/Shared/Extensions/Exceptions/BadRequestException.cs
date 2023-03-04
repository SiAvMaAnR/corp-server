namespace CSN.Domain.Shared.Extensions.Exceptions
{
    public class BadRequestException : Exception
    {
        public int Status { get; } = 400;

        public BadRequestException(string message) : base(message)
        {

        }
    }
}
