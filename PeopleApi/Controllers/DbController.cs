using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace PeopleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DbController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly IHubContext<MyHub> _hubContext;

        public DbController(MongoDbContext context, IHubContext<MyHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet("getData")]
        public IActionResult GetData()
        {
            var data = _context.MyDataCollection.Find(_ => true).ToList();
            return Ok(data);
        }

        [HttpPost("updateData")]
        public async Task<IActionResult> UpdateData([FromBody] MyData newData)
        {
            newData.UpdatedAt = DateTime.UtcNow;
            // Get exact item by id
            var item = Builders<MyData>.Filter.Eq(d => d.Id, newData.Id);

            // Replaces Old with new
            var update = _context.MyDataCollection.ReplaceOne(item, newData, new ReplaceOptions { IsUpsert = true });

            if (update.IsAcknowledged)
            {
                var updatedData = _context.MyDataCollection.Find(_ => true).ToList();
                await _hubContext.Clients.All.SendAsync("UpdateData", updatedData);
            }

            return Ok(update);
        }

        // This Adds data to the DB
        [HttpPost("addData")]
        public async Task<IActionResult> AddData([FromBody] MyData newData)
        {
            //Generates MongoDb ID
            newData.Id = ObjectId.GenerateNewId().ToString();
            newData.UpdatedAt = DateTime.UtcNow;
            _context.MyDataCollection.InsertOne(newData);

            var updatedData = _context.MyDataCollection.Find(_ => true).ToList();
            await _hubContext.Clients.All.SendAsync("UpdateData", updatedData);

            return Ok(newData);
        }

        //Deletes Data based on ID
        [HttpPost("deleteData")]
        public async Task<IActionResult> DeleteData([FromBody] string id)
        {
            var filter = Builders<MyData>.Filter.Eq(d => d.Id, id);
            var result = _context.MyDataCollection.DeleteOne(filter);

            if (result.IsAcknowledged)
            {
                var updatedData = _context.MyDataCollection.Find(_ => true).ToList();
                await _hubContext.Clients.All.SendAsync("UpdateData", updatedData);
            }

            return Ok(result);
        }

    }
}
