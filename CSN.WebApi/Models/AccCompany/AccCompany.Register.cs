namespace CSN.WebApi.Models.AccCompany
{
    public class AccCompanyRegister
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Image { get; set; } = null!;
        public string? Description { get; set; }
    }
}
