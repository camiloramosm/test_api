using PropertySystem.Application.Abstractions.Clock;

namespace PropertySystem.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
