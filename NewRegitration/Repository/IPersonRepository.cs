using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewRegitration.Authentication;
using NewRegitration.Models;

namespace Registration.Repository
{
    public interface IPersonRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> UpdateUserAsync(string email, string newEmail, string newFirstName, string newLastName);
        Task<IdentityResult> DeleteUserAsync(string email);
    }
}
