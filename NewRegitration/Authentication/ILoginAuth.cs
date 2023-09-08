using NewRegitration.Models;

namespace NewRegitration.Authentication
{
    public interface ILoginAuth
    {
        Task<string> LoginAsync(LoginDTO loginDTO);
    }
}
