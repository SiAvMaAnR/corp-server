namespace CSN.WebApi.Models.Company
{
    public class CompanyAdd
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}
