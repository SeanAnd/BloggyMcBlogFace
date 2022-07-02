using NetCoreWithReactAndBasicAuth.Core.Models;

namespace NetCoreWithReactAndBasicAuth.Core.Services
{
    public interface IUserService
    {
        Task<User?> Authenticate(string username, string password);
        Task<User?> Signup(User user);
    }
}