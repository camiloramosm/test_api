using PropertySystem.Domain.Properties;
using MongoDB.Driver;

namespace PropertySystem.Infrastructure.Repositories;

internal sealed class MongoOwnerRepository : IOwnerRepository
{
    private readonly IMongoCollection<Owner> _owners;

    public MongoOwnerRepository(IMongoDatabase database)
    {
        _owners = database.GetCollection<Owner>("owners");
    }

    public async Task<Owner?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _owners
            .Find(o => o.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Add(Owner owner)
    {
        _owners.InsertOne(owner);
    }

    public void Update(Owner owner)
    {
        _owners.ReplaceOne(o => o.Id == owner.Id, owner);
    }

    public void Delete(Owner owner)
    {
        _owners.DeleteOne(o => o.Id == owner.Id);
    }
}
