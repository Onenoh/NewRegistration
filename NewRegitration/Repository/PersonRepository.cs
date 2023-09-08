using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewRegitration.Authentication;
using NewRegitration.Models;

namespace Registration.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO)
        {

            var user = new ApplicationUser()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            return await _userManager.CreateAsync(user, registerDTO.Password);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }



        public async Task<IdentityResult> UpdateUserAsync(string email, string newEmail, string newFirstName, string newLastName)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            user.Email = newEmail;
            user.FirstName = newFirstName;
            user.LastName = newLastName;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _userManager.DeleteAsync(user);
        }
    }
}
