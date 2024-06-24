using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cine_search.Application.Interfaces;
using api_cine_search.Domain.Models;
using api_cine_search.Domain.UseCases;
using api_cine_search.Infra.Databases;

namespace api_cine_search.Application.UseCases
{
  public class RetrieveUser : IRetrieveUser
  {
    private readonly IUserRepository _userRepository;
    public RetrieveUser(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<UserModel> Retrieve(string email)
    {
      var user = await _userRepository.FindOne(email);
      if (user == null)
      {
        throw new Exception("User not found");
      }
      return user;
    }
  }
}