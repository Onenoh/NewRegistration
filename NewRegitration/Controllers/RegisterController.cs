using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewRegitration.Authentication;
using NewRegitration.Models;
using Registration.Repository;
using System.Threading.Tasks;

namespace NewRegitration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterController(IPersonRepository personRepository, UserManager<ApplicationUser> userManager)
        {
            _personRepository = personRepository;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            
            var result = await _personRepository.RegisterAsync(registerDTO);

            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return BadRequest("Password and confirm password do not match.");
            }

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _personRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            var result = await _personRepository.UpdateUserAsync(
                updateUserDTO.Email,
                updateUserDTO.NewEmail,
                updateUserDTO.NewFirstName,
                updateUserDTO.NewLastName
            );

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
            var result = await _personRepository.DeleteUserAsync(email);

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
