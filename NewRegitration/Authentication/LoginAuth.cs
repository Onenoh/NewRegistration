using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewRegitration.Models;
using NewRegitration.Repository;

namespace NewRegitration.Authentication
{
    public class LoginAuth : ILoginAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginAuth(UserManager<ApplicationUser> userManager, ITokenGenerator tokenGenerator)
        {
            this._userManager = userManager;
            this._tokenGenerator = tokenGenerator;
        }

        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
               
                    var token = await _tokenGenerator.GenerateTokenAsync(user);

                    return token;
            }
           
            throw new InvalidOperationException("Invalid credentials");

        }


    }
}

