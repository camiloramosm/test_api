using PropertySystem.Domain.Users;

namespace PropertySystem.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        CancellationToken cancellationToken = default);
}
