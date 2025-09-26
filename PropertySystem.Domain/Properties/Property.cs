using PropertySystem.Domain.Abstractions;

namespace PropertySystem.Domain.Properties;

public sealed class Property : Entity
{
    private readonly List<PropertyImage> _images = new();
    private readonly List<PropertyTrace> _traces = new();

    private Property()
    {
    }

    private Property(Guid id, string name, string address, decimal price, string codeInternal, int year, Guid ownerId) : base(id)
    {
        Name = name;
        Address = address;
        Price = price;
        CodeInternal = codeInternal;
        Year = year;
        OwnerId = ownerId;
    }

    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string CodeInternal { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public Guid OwnerId { get; private set; }

    public IReadOnlyCollection<PropertyImage> Images => _images.ToList();
    public IReadOnlyCollection<PropertyTrace> Traces => _traces.ToList();

    public static Property Create(string name, string address, decimal price, string codeInternal, int year, Guid ownerId)
    {
        return new Property(
            Guid.CreateVersion7(),
            name,
            address,
            price,
            codeInternal,
            year,
            ownerId
        );
    }

    public void Update(string name, string address, decimal price, string codeInternal, int year)
    {
        Name = name;
        Address = address;
        Price = price;
        CodeInternal = codeInternal;
        Year = year;
    }

    public void AddImage(string file, bool enabled = true)
    {
        var image = PropertyImage.Create(Id, file, enabled);
        _images.Add(image);
    }

    public void AddTrace(DateTime dateSale, string name, decimal value, decimal tax)
    {
        var trace = PropertyTrace.Create(Id, dateSale, name, value, tax);
        _traces.Add(trace);
    }
}
