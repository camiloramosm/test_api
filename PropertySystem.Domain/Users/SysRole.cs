namespace PropertySystem.Domain.Users;

public sealed class SysRole
{
    public static readonly SysRole Registered = new(1, "Registered");

    public SysRole(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public ICollection<User> Users { get; init; } = new List<User>();
}
