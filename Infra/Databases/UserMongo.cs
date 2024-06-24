using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cine_search.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api_cine_search.Infra.Databases
{
  public class UserMongo
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

  }
}