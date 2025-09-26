namespace PropertySystem.Api.Controllers.User;

public sealed record RegisterUserRequest
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
    );
