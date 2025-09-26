using PropertySystem.Domain.Users;

namespace PropertySystem.Infrastructure.Authorization;

public sealed class UserRolesResponse
{
    public Guid Id { get; init; }
    public List<SysRole> Roles { get; init; } = [];
}