//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//public class MongoBackgroundService : BackgroundService
//{
//    private readonly MongoChangeStreamService _mongoChangeStreamService;
//    private readonly ILogger<MongoBackgroundService> _logger;

//    public MongoBackgroundService(MongoChangeStreamService mongoChangeStreamService, ILogger<MongoBackgroundService> logger)
//    {
//        _mongoChangeStreamService = mongoChangeStreamService;
//        _logger = logger;
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        _logger.LogInformation("MongoDB Change Stream Service Started...");

//        while (!stoppingToken.IsCancellationRequested)
//        {
//            try
//            {
//                await _mongoChangeStreamService.WatchChangesAsync(stoppingToken);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Change Stream Error: {ex.Message}");
//                await Task.Delay(5000, stoppingToken); 
//            }
//        }
//    }
//}