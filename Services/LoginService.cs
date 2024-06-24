using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cine_search.Helpers;
using api_cine_search.Configurations;
using api_cine_search.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_cine_search.Services
{
  public class LoginService
  {
    private readonly IMongoCollection<User> _users;

    private readonly Authenticate _auth;
    public LoginService(IOptions<DatabaseSettings> databaseSettings, Authenticate auth)
    {
      _auth = auth;
      var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
      var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
      _users = mongoDb.GetCollection<User>(databaseSettings.Value.SetCollectionName("Users"));
    }
    public async Task<string> Login(string email, string password)
    {
      if (email == null || password == null)
      {
        throw new Exception("Invalid email or password");
      }
      var auth = await _auth.AuthenticateAsync(email, password);
      if (auth == false)
      {
        throw new Exception("Invalid email or password");
      }
      var user = await _users.Find(x => x.Email == email).FirstOrDefaultAsync();
      if (user == null || user.Id == null || user.Email == null)
      {
        throw new Exception("User not found");
      }
      var token = _auth.GenerateToken(user.Id, user.Email);
      return token;
    }
  }
}