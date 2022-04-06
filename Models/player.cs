using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlayerApiMongo.Models;

public class Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? nickname { get; set; }

    public string? vocation { get; set; }

    public int level { get; set; }
}


public class PlayersDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PlayersCollectionName { get; set; } = null!;
}