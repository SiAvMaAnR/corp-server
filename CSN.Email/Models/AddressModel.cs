namespace CSN.Email.Models;

public class AddressModel
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = null!;

    public AddressModel(string name, string email)
    {
        Name = name;
        Email = email;
    }
}
