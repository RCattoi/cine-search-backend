using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api_cine_search.Configurations;
using api_cine_search.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace api_cine_search.Helpers
{
  public class Authenticate : IAuthenticate
  {
    private readonly IMongoCollection<User> _users;
    private readonly IMongoCollection<UserSaltDetails> _salt;
    private readonly IConfiguration _configuration;
    public Authenticate(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration)
    {
      _configuration = configuration;
      var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
      var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
      _users = mongoDb.GetCollection<User>(databaseSettings.Value.SetCollectionName("Users"));
      _salt = mongoDb.GetCollection<UserSaltDetails>(databaseSettings.Value.SetCollectionName("UserSaltDetails"));
    }
    public async Task<bool> AuthenticateAsync(string email, string password)
    {
      var user = await UserExistsAsync(email);
      if (user == null)
      {
        return false;
      }
      var salt = await _salt.Find(x => x.UserId == user.Id).FirstOrDefaultAsync();
      if (salt == null || salt.Salt == null || salt.SaltSize == 0)
      {
        return false;
      }
      User userData = await _users.Find(x => x.Email == email).FirstOrDefaultAsync();
      var passwordMatch = PasswordHasher.VerifyPassword(userData.Password, password, salt.Salt, salt.SaltSize);

      if (!passwordMatch)
      {
        return false;
      }

      return true;
    }

    public string GenerateToken(string id, string email)
    {
      var claims = new[]
      {
        new Claim("id", id.ToString()),
        new Claim("email", email),
        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
      };

      var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secretKey"] ?? ""));

      var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

      var expiration = DateTime.UtcNow.AddHours(1);

      JwtSecurityToken token = new JwtSecurityToken(
        issuer: _configuration["Jwt:issuer"],
        audience: _configuration["Jwt:audience"],
        claims: claims,
        expires: expiration,
        signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<User> UserExistsAsync(string email)
    {
      var userExists = await _users.Find(x => x.Email == email).FirstOrDefaultAsync();
      if (userExists == null)
      {
        throw new Exception("User not found");
      }
      return userExists;
    }
  }
}