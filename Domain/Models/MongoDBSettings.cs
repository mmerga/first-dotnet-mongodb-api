using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstNetMongo.Domain.Models;

public class MongoDBSettings
{
    public string ConnectionURI { get; set; } = null!;
    public string DataBaseProducts { get; set; } = null!;
    public string CollectionProducts { get; set; } = null!;
    public string DataBasePlanets { get; set; } = null!;
    public string CollectionPlanets { get; set; } = null!;
}
