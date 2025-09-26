namespace PropertySystem.Domain.Users
{
    public record Role
    {
        public Guid Value { get; private set; }

        public Role(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Role cannot be empty");

            Value = value;
        }
    }
}
