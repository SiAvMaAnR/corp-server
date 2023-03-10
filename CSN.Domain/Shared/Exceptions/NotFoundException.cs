namespace CSN.Domain.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public int Status { get; } = 404;

        public NotFoundException(string message) : base(message)
        {

        }
    }
}
