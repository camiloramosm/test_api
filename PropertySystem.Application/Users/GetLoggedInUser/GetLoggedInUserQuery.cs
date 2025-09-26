using PropertySystem.Application.Abstractions.Messaging;

namespace PropertySystem.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;