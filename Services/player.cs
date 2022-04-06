using PlayerApiMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PlayerApiMongo.Services;

public class PlayersService
{
    private readonly IMongoCollection<Player> _playersCollection;

    public PlayersService(
        IOptions<PlayersDatabaseSettings> playersDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            playersDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            playersDatabaseSettings.Value.DatabaseName);

        _playersCollection = mongoDatabase.GetCollection<Player>(
            playersDatabaseSettings.Value.PlayersCollectionName);
    }

    public async Task<List<Player>> GetAsync() =>
        await _playersCollection.Find(_ => true).ToListAsync();

    public async Task<Player?> GetAsync(string id) =>
        await _playersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Player newPlayer) =>
        await _playersCollection.InsertOneAsync(newPlayer);

    public async Task UpdateAsync(string id, Player updatedPlayer) =>
        await _playersCollection.ReplaceOneAsync(x => x.Id == id, updatedPlayer);

    public async Task RemoveAsync(string id) =>
        await _playersCollection.DeleteOneAsync(x => x.Id == id);
}