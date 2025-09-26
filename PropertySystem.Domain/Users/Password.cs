namespace PropertySystem.Domain.Users;

public record Password
{
    public string Value { get; set; }
    public Password(string password)
    {
        if (password.Length < 8)
            throw new ArgumentException("password must be at least 8 characters long.", nameof(password));

        if (password.Length > 50)
            throw new ArgumentException("password must be at most 50 characters long.", nameof(password));

        Value = password;
    }
}
