namespace PropertySystem.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.Users.UserContact recipient, string subject, string body);
}
