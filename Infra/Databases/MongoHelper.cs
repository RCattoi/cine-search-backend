using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace api_cine_search.Infra.Databases
{
  public class MongoHelper
  {
    private readonly IMongoDatabase _mongoDb;
    public MongoHelper(string connectionString, string databaseName)
    {
      var mongoClient = new MongoClient(connectionString);
      _mongoDb = mongoClient.GetDatabase(databaseName);

    }
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
      return _mongoDb.GetCollection<T>(collectionName);
    }
  }
}