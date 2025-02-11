using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PeopleApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MongoDB settings from configuration
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoSettings"));

// Register MongoDbContext as a singleton service
builder.Services.AddSingleton<MongoDbContext>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7013", "https://localhost:7214")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

// Add other services
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<MyHub>("/myHub");

app.Run();
