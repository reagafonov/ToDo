using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace ToDo.WebApi.Domain;

/// <summary>
/// Содержит коллекции для доступа к данным
/// </summary>
/// <param name="database"></param>
public class MongoContext(IMongoDatabase database)
{
    /// <summary>
    /// Коллекция больших файлов
    /// </summary>
    public Lazy<GridFSBucket> GridFsBucket { get; } 
        = new Lazy<GridFSBucket>(new GridFSBucket(database));
    
    /// <summary>
    /// Коллекция маленьких файлов <=16 MB
    /// </summary>
    public Lazy<IMongoCollection<BsonDocument>> DocumentCollection { get; } 
        = new Lazy<IMongoCollection<BsonDocument>>(database.GetCollection<BsonDocument>("files"));
}