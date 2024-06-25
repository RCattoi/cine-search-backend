using Microsoft.AspNetCore.Mvc;
using api_cine_search.Models;
using api_cine_search.Services;

namespace api_cine_search.Controllers
{
  [ApiController]
  [Route("api/login")]
  [Tags("Login")]
  public class LoginController : ControllerBase
  {
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
      _loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] UserModel user)
    {
      try
      {
        var token = await _loginService.Login(user.Email, user.Password);
        return Ok(token);
      }
      catch (Exception ex)
      {
        return Unauthorized(ex.Message);
      }
    }
  }
}