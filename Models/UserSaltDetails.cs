using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace api_cine_search.Models
{
  public class UserSaltDetails
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public byte[]? Salt { get; set; }
    public int SaltSize { get; set; }
  }
}