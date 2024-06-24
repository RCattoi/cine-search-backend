using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cine_search.Application.Interfaces;
using api_cine_search.Domain.Models;
using api_cine_search.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace api_cine_search.Infra.Databases
{
  public class UserRepository : IUserRepository
  {
    private readonly MongoHelper _mongoHelper;
    public UserRepository(MongoHelper mongoHelper)
    {
      _mongoHelper = mongoHelper;
    }

    async Task<UserModel?> IUserRepository.FindOne(string email)
    {
      UserModel user = await _mongoHelper.GetCollection<UserModel>("users").Find(user => user.Email == email).FirstOrDefaultAsync();

      return new UserModel
      {
        Id = user.Id,
        Email = user.Email,
        Name = user.Name,
        Password = user.Password
      };
    }

  }
}