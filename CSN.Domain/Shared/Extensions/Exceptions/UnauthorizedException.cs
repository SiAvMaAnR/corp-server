namespace CSN.Domain.Extensions.CustomExceptions
{
    public class UnauthorizedException: Exception
    {
        public int Status { get; } = 401;

        public UnauthorizedException(string message) : base(message)
        {

        }
    }
}
