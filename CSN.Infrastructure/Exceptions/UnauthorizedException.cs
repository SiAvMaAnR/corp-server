namespace CSN.Infrastructure.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public int Status { get; } = 401;

        public UnauthorizedException(string message) : base(message)
        {

        }
    }
}
