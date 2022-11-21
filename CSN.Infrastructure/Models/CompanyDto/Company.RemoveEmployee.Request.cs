namespace CSN.Infrastructure.Models.CompanyDto;

public class CompanyRemoveEmployeeRequest
{
    public int Id { get; set; }

    public CompanyRemoveEmployeeRequest() { }
    public CompanyRemoveEmployeeRequest(int id)
    {
        this.Id = id;
    }
}
