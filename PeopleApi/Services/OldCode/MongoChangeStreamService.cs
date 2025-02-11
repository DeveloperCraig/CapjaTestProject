//using MongoDB.Driver;
//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//public class MongoChangeStreamService
//{
//    private readonly IMongoCollection<MyData> _collection;
//    private readonly IHubContext<RealtimeHub> _hubContext;

//    public MongoChangeStreamService(IMongoClient client, IHubContext<RealtimeHub> hubContext)
//    {
//        var database = client.GetDatabase("RealTimeDB");
//        _collection = database.GetCollection<MyData>("RealTimeCollection");
//        _hubContext = hubContext;
//    }

//    public async Task WatchChangesAsync(CancellationToken stoppingToken)
//    {
//        var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<MyData>>().Match(_ => true);

//        using var cursor = await _collection.WatchAsync(pipeline, cancellationToken: stoppingToken);

//        while (await cursor.MoveNextAsync(stoppingToken))
//        {
//            foreach (var change in cursor.Current)
//            {
//                if (change.FullDocument != null)
//                {
//                    await _hubContext.Clients.All.SendAsync("ReceiveUpdate", change.FullDocument, stoppingToken);
//                }
//            }
//        }
//    }
//}