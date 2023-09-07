using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewRegitration.Authentication;
using NewRegitration.Models;
using System.Threading.Tasks;

namespace NewRegitration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return BadRequest("Password and confirm password do not match.");
            }

            var user = new ApplicationUser()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetUserById(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> UpdateUser(string email, string newEmail, string newFirstName, string newLastName)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            
            user.Email = newEmail;
            user.FirstName = newFirstName;
            user.LastName = newLastName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("User updated successfully.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("User deleted successfully.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

    }
}
