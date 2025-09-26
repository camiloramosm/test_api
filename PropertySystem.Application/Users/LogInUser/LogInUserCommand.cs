using PropertySystem.Application.Abstractions.Messaging;

namespace PropertySystem.Application.Users.LogInUser;

public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;