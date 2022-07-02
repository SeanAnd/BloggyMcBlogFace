using NetCoreWithReactAndBasicAuth.Core.Models;

namespace NetCoreWithReactAndBasicAuth.Core.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> Get();
        Task<Post?> Create(Post post);
        Task<Post?> GetById(int id);
        Task<Post> Update(Post post);
        Task<Post> Delete(Post post);
    }
}
