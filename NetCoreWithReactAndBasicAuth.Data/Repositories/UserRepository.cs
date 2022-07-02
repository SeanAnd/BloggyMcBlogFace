using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreWithReactAndBasicAuth.Core.Repositories;
using NetCoreWithReactAndBasicAuth.Data.Entities;

namespace NetCoreWithReactAndBasicAuth.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext context;
        protected readonly IMapper mapper;
        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Core.Models.User?> Authenticate(string username, string password)
        {
            User? user = await context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }
            else
            {
                return MapToCore(user);
            }
        }

        public async Task<Core.Models.User?> Signup(Core.Models.User user)
        {
            //TODO: add hashing and salting to password.
            var exists = await Exists(user.Username);
            if (!exists)
            {
                var entity = MapToData(user);
                context.Add(entity);
                await context.SaveChangesAsync();
                return MapToCore(entity);
            }
            else
            {
                return null;
            }
        }

        private async Task<bool> Exists(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if(user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Core.Models.User MapToCore(User entity)
        {
            return mapper.Map<Core.Models.User>(entity);
        }

        private User MapToData(Core.Models.User coreModel)
        {
            return mapper.Map<User>(coreModel);
        }
    }
}
