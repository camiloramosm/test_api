using PropertySystem.Domain.Abstractions;

namespace PropertySystem.Domain.Properties;

public sealed class PropertyImage : Entity
{
    private PropertyImage()
    {
    }

    private PropertyImage(Guid id, Guid propertyId, string file, bool enabled) : base(id)
    {
        PropertyId = propertyId;
        File = file;
        Enabled = enabled;
    }

    public Guid PropertyId { get; private set; }
    public string File { get; private set; } = string.Empty;
    public bool Enabled { get; private set; }

    public static PropertyImage Create(Guid propertyId, string file, bool enabled = true)
    {
        return new PropertyImage(
            Guid.CreateVersion7(),
            propertyId,
            file,
            enabled
        );
    }

    public void Update(string file, bool enabled)
    {
        File = file;
        Enabled = enabled;
    }
}
