using NetCoreWithReactAndBasicAuth.Core.Repositories;

namespace NetCoreWithReactAndBasicAuth.Core.Services
{
    public class PostService : IPostService
    {
        protected readonly IPostRepository repo;

        public PostService(IPostRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<Core.Models.Post>> Get()
        {
            return await repo.Get();
        }

        public async Task<Core.Models.Post?> GetById(int id)
        {
            return await repo.GetById(id);
        }

        public async Task<Core.Models.Post?> Create(Core.Models.Post post)
        {
            return await repo.Create(post);
        }

        public async Task<Core.Models.Post> Update(Core.Models.Post post)
        {
            return await repo.Update(post);
        }

        public async Task<Core.Models.Post> Delete(Core.Models.Post post)
        {
            return await repo.Delete(post);
        }
    }
}