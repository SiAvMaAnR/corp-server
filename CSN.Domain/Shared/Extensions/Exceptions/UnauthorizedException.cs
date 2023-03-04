namespace CSN.Domain.Shared.Extensions.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public int Status { get; } = 401;

        public UnauthorizedException(string message) : base(message)
        {

        }
    }
}
