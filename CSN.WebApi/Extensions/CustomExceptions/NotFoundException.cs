namespace CSN.WebApi.Extensions.CustomExceptions
{
    public class NotFoundException: Exception
    {
        public int Status { get; } = 404;

        public NotFoundException(string message) : base(message)
        {

        }
    }
}
