using NetCoreWithReactAndBasicAuth.Core.Models;

namespace NetCoreWithReactAndBasicAuth.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User?> Authenticate(string username, string password);
        Task<User?> Signup(User user);
    }
}
