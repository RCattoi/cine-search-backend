using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api_cine_search.Models
{
    public class User
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string? Id { get; set; }
      
      public string? Name { get; set; }
      public required string Email { get; set; }
      public required string Password { get; set; }
    }
}