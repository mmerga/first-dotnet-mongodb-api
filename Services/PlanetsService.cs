using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

using FirstNetMongo.Domain.Models;

namespace FirstNetMongo.Services;

public class PlanetsService
{
    private readonly IMongoCollection<Planets> _planetsCollection;

    public PlanetsService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        var database = client.GetDatabase(mongoDBSettings.Value.DataBasePlanets);
        _planetsCollection = database.GetCollection<Planets>(mongoDBSettings.Value.CollectionPlanets);
    }

    public async Task<IEnumerable<Planets>> GetAsync()
    {
        return await _planetsCollection.Find(_ => true).ToListAsync();
    }
}
