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
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        var database = client.GetDatabase(mongoDBSettings.Value.DataBaseProducts);
        _usersCollection = database.GetCollection<Users>(mongoDBSettings.Value.CollectionProducts);
        // _usersCollection = database.GetCollection<BsonDocument>(mongoDBSettings.Value.CollectionName);
        // generic Parameters if you do not know with class or interface you are going to use --> new BsonDocument()
        // Criar o índice único para o campo CPF
        var indexKeysDefinition = Builders<Users>.IndexKeys.Ascending(x => x.Email);
        CreateIndexOptions indexOptions = new CreateIndexOptions { Unique = true };
        CreateIndexModel<Users> indexModel = new CreateIndexModel<Users>(indexKeysDefinition, indexOptions);

        _usersCollection.Indexes.CreateOne(indexModel);
    }

    public async Task<List<Users>> GetAsync()
    {
        return await _usersCollection.Find(_ => true).ToListAsync();
        // return await _usersCollection.Find(new BsonDocument()).ToListAsync();
        // _usersCollection.Find('true' or 'your filter').Skip(skipAmount).Limit(limitAmount).ToListAsync();
        // if you want to get a especific amount of data you can use skip and limit
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
        // var builder = Builders<Users>.Filter
        // var filter = builder.Eq(x => x.Id, id) & builder.Gt('Age', 18) & builder.Lt('Age', 30);
        // another way to make a filter -> Biulder class

        await _usersCollection.DeleteOneAsync(filter);
        return;
    }
}
