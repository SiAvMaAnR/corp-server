namespace CSN.WebApi.Controllers.Models.User
{
    public class UserGetAll
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? SearchFilter { get; set; }
    }
}