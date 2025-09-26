using PropertySystem.Domain.Abstractions;

namespace PropertySystem.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;