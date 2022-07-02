using NetCoreWithReactAndBasicAuth.Core.Models;

namespace NetCoreWithReactAndBasicAuth.Core.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> Get();
        Task<Post?> Create(Post post);
        Task<Post> Update(Post post);
        Task<Post> Delete(Post post);
        Task<Post?> GetById(int id);
    }
}