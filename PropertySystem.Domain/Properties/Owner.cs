using PropertySystem.Domain.Abstractions;

namespace PropertySystem.Domain.Properties;

public sealed class Owner : Entity
{
    private Owner()
    {
    }

    private Owner(Guid id, string name, string address, string? photo, DateTime birthday) : base(id)
    {
        Name = name;
        Address = address;
        Photo = photo;
        Birthday = birthday;
    }

    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string? Photo { get; private set; }
    public DateTime Birthday { get; private set; }

    public static Owner Create(string name, string address, string? photo, DateTime birthday)
    {
        return new Owner(
            Guid.CreateVersion7(),
            name,
            address,
            photo,
            birthday
        );
    }

    public void Update(string name, string address, string? photo, DateTime birthday)
    {
        Name = name;
        Address = address;
        Photo = photo;
        Birthday = birthday;
    }
}
