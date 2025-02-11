using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PeopleApi.Models;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoDbSettings _options;

    // This will get the configerations from the AppSettings
    public MongoDbContext(IOptions<MongoDbSettings> options)
    {
        _options = options.Value;
        var client = new MongoClient(_options.ConnectionString);
        _database = client.GetDatabase(_options.Database);
    }

    public IMongoCollection<MyData> MyDataCollection => _database.GetCollection<MyData>(_options.Collection);
}
