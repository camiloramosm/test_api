using PropertySystem.Domain.Abstractions;

namespace PropertySystem.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
       string email,
       string password,
       CancellationToken cancellationToken = default);
}
