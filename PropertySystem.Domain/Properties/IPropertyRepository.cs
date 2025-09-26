using PropertySystem.Domain.Properties;

namespace PropertySystem.Domain.Properties;

public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Property>> GetFilteredPropertiesAsync(
        string? name = null,
        string? address = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        CancellationToken cancellationToken = default);
    void Add(Property property);
    void Update(Property property);
    void Delete(Property property);
}
