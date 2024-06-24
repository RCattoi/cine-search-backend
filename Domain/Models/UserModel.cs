using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cine_search.Infra.Databases;


namespace api_cine_search.Domain.Models
{
  public class UserModel : UserMongo
  {
    public string? Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
  }
}