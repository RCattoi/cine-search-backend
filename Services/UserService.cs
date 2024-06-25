using api_cine_search.Configurations;
using api_cine_search.Helpers;
using api_cine_search.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace api_cine_search.Services
{
  public class UserService
  {
    private readonly IMongoCollection<UserModel> _users;
    private readonly IMongoCollection<UserSaltDetails> _salt;

    public UserService(IOptions<DatabaseSettings> databaseSettings)
    {
      var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
      var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
      _users = mongoDb.GetCollection<UserModel>(databaseSettings.Value.SetCollectionName("Users"));
      _salt = mongoDb.GetCollection<UserSaltDetails>(databaseSettings.Value.SetCollectionName("UserSaltDetails"));
    }

    public async Task<List<UserModel>> GetAsync() => await _users.Find(filter: _ => true).ToListAsync();

    public async Task<UserModel> GetAsync(int id) => await _users.Find<UserModel>(filter: user => user.Id == id).FirstOrDefaultAsync();

    public async Task<UserModel> CreateAsync(UserModel user)
    {
      var alreadyExists = await _users.Find(x => x.Email == user.Email).FirstOrDefaultAsync();
      if (alreadyExists != null)
      {
        throw new Exception($"User already exists with email {user.Email}");

      }

      string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
      if (!Regex.IsMatch(user.Email, emailPattern))
      {
        throw new Exception("Invalid email");
      }
      if (string.IsNullOrEmpty(user.Name))
      {
        throw new Exception("Name is required");
      }
      if (string.IsNullOrEmpty(user.Password))
      {
        throw new Exception("Password is required");
      }

      await _users.InsertOneAsync(user);
      return user;
    }

    public async Task<UserSaltDetails> CreateSaltAsync(UserSaltDetails userSaltDetails)
    {
      await _salt.InsertOneAsync(userSaltDetails);
      return userSaltDetails;
    }
  }
}