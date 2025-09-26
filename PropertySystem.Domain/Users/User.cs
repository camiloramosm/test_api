using PropertySystem.Domain.Abstractions;
using PropertySystem.Domain.Users.Events;

namespace PropertySystem.Domain.Users;

public sealed class User : Entity
{
    private readonly List<SysRole> _roles = new();
    private User()
    {
    }

    private User(Guid id, FirstName firstName, SecondName secondName, LastName lastName, FullName fullName, UserContact userContact, BusinessUnit businessUnit, Password password, Role role, Identification identification, Email email) : base(id)
    {
        FirstName = firstName;
        SecondName = secondName;
        LastName = lastName;
        FullName = fullName;
        UserContact = userContact;
        BusinessUnit = businessUnit;
        Password = password;
        Role = role;
        Identification = identification;
        Email = email;
    }

    public FirstName FirstName { get; private set; }
    public SecondName SecondName { get; private set; }
    public LastName LastName { get; private set; }
    public FullName FullName { get; private set; }
    public UserContact UserContact { get; private set; }
    public BusinessUnit BusinessUnit { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public Identification Identification { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime InactivationDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Email Email { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;

    public IReadOnlyCollection<SysRole> Roles => _roles.ToList();

    public static User Create(
        FirstName firstName,
        SecondName secondName,
        LastName lastName,
        FullName fullName,
        UserContact userContact,
        BusinessUnit businessUnit,
        Password password,
        Role role,
        Identification identification, Email email)
    {
        var user = new User
        (
            Guid.CreateVersion7(),
            firstName,
            secondName,
            lastName,
            fullName,
            userContact,
            businessUnit,
            password,
            role,
            identification,
            email
        );

        user._roles.Add(SysRole.Registered);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        if (string.IsNullOrWhiteSpace(identityId))
        {
            throw new ArgumentException("Identity ID cannot be null or empty.", nameof(identityId));
        }
        IdentityId = identityId;
    }
}
