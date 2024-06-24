using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cine_search.Application;
using api_cine_search.Infra.Databases;
using Microsoft.AspNetCore.Mvc;
using api_cine_search.Application.UseCases;

namespace api_cine_search.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TestController : ControllerBase
  {
    [HttpGet]
    public async Task<IActionResult> Get()
    {

      var mongoHelper = new MongoHelper("mongoconnectionstring", "cine-search");
      var userRepository = new UserRepository(mongoHelper);
      var retrieveUser = new RetrieveUser(userRepository);
      var user = await retrieveUser.Retrieve("rodrigo.costa@gmail.com");

      return Ok(user);
    }
  }
}