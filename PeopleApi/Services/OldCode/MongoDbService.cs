//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//public class MongoDbService
//{
//    private readonly IMongoDatabase _database;

//    public MongoDbService(IMongoClient client)
//    {
//        // Initializes _database with the RealTimeDB database
//        _database = client.GetDatabase("RealTimeDB");
//    }

//    // Example method to get all data from RealTimeCollection
//    public async Task<List<MyData>> GetDataAsync()
//    {
//        var collection = _database.GetCollection<MyData>("RealTimeCollection");
//        var data = await collection.Find(_ => true).ToListAsync();
//        return data;
//    }

//    // Example method to insert data into the collection
//    public async Task InsertDataAsync(MyData data)
//    {
//        var collection = _database.GetCollection<MyData>("RealTimeCollection");
//        await collection.InsertOneAsync(data);
//    }
//}