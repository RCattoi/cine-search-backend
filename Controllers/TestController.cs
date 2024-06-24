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

      var mongoHelper = new MongoHelper("mongodb+srv://cattoi:UDYXG873GvONSIWx@cine-search-db.wssoj9j.mongodb.net/?retryWrites=true&w=majority&appName=cine-search-db", "cine-search");
      var userRepository = new UserRepository(mongoHelper);
      var retrieveUser = new RetrieveUser(userRepository);
      var user = await retrieveUser.Retrieve("rodrigo.costa@gmail.com");

      return Ok(user);
    }
  }
}