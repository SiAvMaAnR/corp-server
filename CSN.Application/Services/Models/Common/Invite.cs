using CSN.Domain.Shared.Enums;

public class Invite
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Email { get; set; } = null!;
    public EmployeePost Post { get; set; }
    public Invite() { }
    public Invite(int id, string email, int companyId, EmployeePost post)
    {
        this.Id = id;
        this.Email = email;
        this.CompanyId = companyId;
        this.Post = post;
    }
}
