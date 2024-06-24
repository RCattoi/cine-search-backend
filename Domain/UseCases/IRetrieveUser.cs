using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cine_search.Domain.Models;
using api_cine_search.Infra.Databases;

namespace api_cine_search.Domain.UseCases
{
  public interface IRetrieveUser
  {
    public abstract Task<UserModel> Retrieve(string email);
  }
}