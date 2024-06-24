using api_cine_search.Helpers;
using api_cine_search.Models;
using api_cine_search.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_cine_search.Controllers
{
  [ApiController]
  [Route("api/user")]
  [Tags("User")]
  public class UsersControllers : ControllerBase
  {
    private readonly UserService _userService;

    public UsersControllers(UserService userService)
    {
      _userService = userService;

    }

    [HttpGet("id:length(24)")]
    // [Route("get-user")]
    public async Task<IActionResult> Get(string id)
    {
      var user = await _userService.GetAsync(id);
      if (user is null)
      {
        return NotFound();
      }
      else
      {
        return Ok(user);
      }
    }

    [HttpPost]
    [Route("create-user")]
    public async Task<IActionResult> Post([FromBody] Models.User user)
    {
      try
      {
        var result = PasswordHasher.HashPassword(user.Password);
        user.Password = result.hashedPassword;
        var createdUser = await _userService.CreateAsync(user);
        CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        var salt = new UserSaltDetails
        {
          UserId = createdUser.Id,
          Salt = result.salt,
          SaltSize = result.saltSize
        };
        await _userService.CreateSaltAsync(salt);
        return Ok(createdUser);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    
  }

  internal class UserSaltDetailsService
  {
  }
}