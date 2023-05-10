namespace CSN.WebApi.Controllers.Models.User
{
    public class UserGetAll
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchFilter { get; set; }
    }
}