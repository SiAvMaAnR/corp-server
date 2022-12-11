namespace CSN.Infrastructure.Models.EmployeeControlDto;

public class EmployeeControlRemoveRequest
{
    public int Id { get; set; }

    public EmployeeControlRemoveRequest() { }
    public EmployeeControlRemoveRequest(int id)
    {
        this.Id = id;
    }
}
