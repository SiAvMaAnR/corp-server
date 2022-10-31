namespace CSN.WebApi.Extensions.CustomExceptions
{
    public class ForbiddenException : Exception
    {
        public int Status { get; } = 403;

        public ForbiddenException(string message) : base(message)
        {

        }
    }
}
