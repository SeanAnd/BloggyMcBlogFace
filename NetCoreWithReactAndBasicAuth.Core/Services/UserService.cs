using NetCoreWithReactAndBasicAuth.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWithReactAndBasicAuth.Core.Services
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository repo;

        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Core.Models.User?> Authenticate(string username, string password)
        {
            return await repo.Authenticate(username, password);
        }

        public async Task<Core.Models.User?> Signup(Core.Models.User user)
        {
            return await repo.Signup(user);
        }
    }
}