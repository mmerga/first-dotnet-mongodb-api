using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

using FirstNetMongo.Domain.Models;

namespace FirstNetMongo.Services;

public class MongoDBServices
{
    private readonly IMongoCollection<Users> _usersCollection;

    public MongoDBServices(IOptions<MongoDBSettings> mongoDBSettings)
    {
        var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _usersCollection = database.GetCollection<Users>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<Users>> GetAsync()
    {
        return await _usersCollection.Find(_ => true).ToListAsync();
        // return await _usersCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Users> CreateAsync(Users user)
    {
        await _usersCollection.InsertOneAsync(user);
        return user;
    }


    public async Task<Users> PutAsync(string id, Users user)
    {
        var filter = Builders<Users>.Filter.Eq("Id", id);
        var old = _usersCollection.Find(filter).First();

        user.Id = old.Id;
        await _usersCollection.ReplaceOneAsync(filter, user);
        return user;
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<Users>.Filter.Eq("Id", id);

        await _usersCollection.DeleteOneAsync(filter);
        return;
    }
}
