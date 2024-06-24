using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cine_search.Domain.Models;
using api_cine_search.Infra.Databases;

namespace api_cine_search.Application.Interfaces
{
  public interface IUserRepository
  {
    Task<UserModel?> FindOne(string email);

    // Task<UserModel?> Create(UserModel user);
  }
}