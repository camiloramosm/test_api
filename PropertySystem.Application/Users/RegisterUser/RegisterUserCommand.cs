using PropertySystem.Application.Abstractions.Messaging;

namespace PropertySystem.Application.Users.RegisterUser;

public sealed record RegisterUserCommand
(
        string Email,
        string FirstName,
        string SecondName,
        string LastName,
        string FullName,
        string Password,
        string Phone,
        string Address,
        int BusinessUnit,
        Guid Role,
        string Identification
) : ICommand<Guid>;
