using NewRegitration.Authentication;

namespace NewRegitration.Repository
{
    public interface ITokenGenerator
    {
        public Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
