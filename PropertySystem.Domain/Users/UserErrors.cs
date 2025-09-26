using PropertySystem.Domain.Abstractions;

namespace PropertySystem.Domain.UserErrors;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.NotFound",
        "The user was not found.");

    public static readonly Error InvalidCredentials = new(
      "User.InvalidCredentials",
      "The provided credentials were invalid");
}
