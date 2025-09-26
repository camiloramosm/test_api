using PropertySystem.Domain.Abstractions;

namespace PropertySystem.Domain.Properties;

public sealed class PropertyTrace : Entity
{
    private PropertyTrace()
    {
    }

    private PropertyTrace(Guid id, Guid propertyId, DateTime dateSale, string name, decimal value, decimal tax) : base(id)
    {
        PropertyId = propertyId;
        DateSale = dateSale;
        Name = name;
        Value = value;
        Tax = tax;
    }

    public Guid PropertyId { get; private set; }
    public DateTime DateSale { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public decimal Tax { get; private set; }

    public static PropertyTrace Create(Guid propertyId, DateTime dateSale, string name, decimal value, decimal tax)
    {
        return new PropertyTrace(
            Guid.CreateVersion7(),
            propertyId,
            dateSale,
            name,
            value,
            tax
        );
    }

    public void Update(DateTime dateSale, string name, decimal value, decimal tax)
    {
        DateSale = dateSale;
        Name = name;
        Value = value;
        Tax = tax;
    }
}
