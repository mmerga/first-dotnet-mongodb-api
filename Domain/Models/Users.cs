using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System.Text.Json.Serialization;

namespace FirstNetMongo.Domain.Models;

// [BsonIgnoreExtraElements]    => ignore extra elements
public class Users
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    // public ObjectId Id { get; set;}

    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [BsonElement("email")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [BsonElement("password")]
    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

    // [BsonExtraElements] => put the others elements in this array
    // public object[] Rest { get; set; }
}
