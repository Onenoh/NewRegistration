using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewRegitration.Authentication;
using NewRegitration.Models;

namespace NewRegitration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAuthController : ControllerBase
    {
        private readonly ILoginAuth _loginAuth;

        public LoginAuthController(ILoginAuth loginAuth)
        {
            this._loginAuth = loginAuth;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var token = await _loginAuth.LoginAsync(loginDTO);

                return Ok(new { Token = token, Message = "Login Successful" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        //[HttpPost("login")]
        //public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDTO)
        //{
        //    var response = await _loginAuth.LoginAsync(loginDTO);
        //    if (response.Succeeded)
        //    {
        //        return Ok(response);
        //    }
        //    return BadRequest(response);
        //}
    }
}
