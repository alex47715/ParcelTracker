using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace MongoLib
{
    public interface IMongoRepository
    {
        IMongoCollection<BsonDocument> GetCollection(string nameOfCollection);
    }
    public class BaseRepository:IMongoRepository
    {
        private readonly string _connectionString;
        IMongoDatabase _database;
        public BaseRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            _connectionString = connectionString;
            var connection = new MongoUrlBuilder(_connectionString);
            MongoClient client = new MongoClient(_connectionString);
            _database = client.GetDatabase(connection.DatabaseName);
        }

        public IMongoCollection<BsonDocument> GetCollection(string nameOfCollection)
        {
            return  _database.GetCollection<BsonDocument>(nameOfCollection);
        }
    }
}
