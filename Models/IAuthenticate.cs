using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_cine_search.Models
{
  public interface IAuthenticate
  {
    Task<bool> AuthenticateAsync(string email, string password);
    Task<User> UserExistsAsync(string email);
    public string GenerateToken(string id, string email);
  }
}