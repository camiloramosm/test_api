using MongoDB.Driver;

namespace PropertySystem.Infrastructure;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoDatabase Database => _database;
}
