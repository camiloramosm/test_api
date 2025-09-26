namespace PropertySystem.Domain.Users;

public sealed record UserContact
{
    public UserContact(string phone, string address)
    {
        Phone = phone;
        Address = address;
    }

    public string Phone { get; set; }
    public string Address { get; set; }
}
