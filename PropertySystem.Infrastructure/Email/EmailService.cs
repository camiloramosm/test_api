using PropertySystem.Application.Abstractions.Email;
using PropertySystem.Domain.Users;

namespace PropertySystem.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(UserContact recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}
