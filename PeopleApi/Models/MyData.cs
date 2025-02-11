using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class MyData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }  // MongoDB's unique ObjectId

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Value")]
    public string Value { get; set; }

    [BsonElement("UpdatedAt")]
    public DateTime UpdatedAt { get; set; }
}