using PropertySystem.Domain.Properties;
using MongoDB.Driver;

namespace PropertySystem.Infrastructure.Repositories;

internal sealed class MongoPropertyRepository : IPropertyRepository
{
    private readonly IMongoCollection<Property> _properties;

    public MongoPropertyRepository(IMongoDatabase database)
    {
        _properties = database.GetCollection<Property>("properties");
    }

    public async Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _properties
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Property>> GetFilteredPropertiesAsync(
        string? name = null,
        string? address = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<Property>.Filter;
        var filters = new List<FilterDefinition<Property>>();

        if (!string.IsNullOrWhiteSpace(name))
        {
            filters.Add(filterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i")));
        }

        if (!string.IsNullOrWhiteSpace(address))
        {
            filters.Add(filterBuilder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(address, "i")));
        }

        if (minPrice.HasValue)
        {
            filters.Add(filterBuilder.Gte(p => p.Price, minPrice.Value));
        }

        if (maxPrice.HasValue)
        {
            filters.Add(filterBuilder.Lte(p => p.Price, maxPrice.Value));
        }

        var filter = filters.Any() ? filterBuilder.And(filters) : filterBuilder.Empty;

        return await _properties
            .Find(filter)
            .ToListAsync(cancellationToken);
    }

    public void Add(Property property)
    {
        _properties.InsertOne(property);
    }

    public void Update(Property property)
    {
        _properties.ReplaceOne(p => p.Id == property.Id, property);
    }

    public void Delete(Property property)
    {
        _properties.DeleteOne(p => p.Id == property.Id);
    }
}
