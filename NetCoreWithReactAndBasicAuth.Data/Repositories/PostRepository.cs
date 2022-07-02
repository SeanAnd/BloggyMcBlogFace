using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreWithReactAndBasicAuth.Core.Repositories;
using NetCoreWithReactAndBasicAuth.Data.Entities;

namespace NetCoreWithReactAndBasicAuth.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        protected readonly ApplicationDbContext context;
        protected readonly IMapper mapper;
        public PostRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Core.Models.Post>> Get()
        {
            return MapToCore(await context.Posts.Include(p => p.Photos).Include(p => p.User)
                .AsNoTracking().ToListAsync()).OrderByDescending(p => p.Id);
        }

        public async Task<Core.Models.Post?> GetById(int id)
        {
            var exists = await Exists(id);
            if(exists)
            {
                return MapToCore(await context.Posts.Include(p => p.Photos).Include(p => p.User)
                    .AsNoTracking().FirstOrDefaultAsync(p => p.Id == id));
            }
            else
            {
                return null;
            }
        }

        public async Task<Core.Models.Post?> Create(Core.Models.Post post)
        {
            var exists = await Exists(post.Id);
            if (!exists)
            {
                var entity = MapToData(post);
                context.Add(entity);
                await context.SaveChangesAsync();
                return MapToCore(entity);
            }
            else
            {
                return null;
            }
        }

        public async Task<Core.Models.Post> Update(Core.Models.Post post)
        {
            var entity = MapToData(post);
            var rows = context.Photos.Where(i => i.PostId == entity.Id).AsNoTracking().ToList();
            var rowsToDelete = rows.Except(entity.Photos).ToList();
            context.Photos.RemoveRange(rowsToDelete);
            context.Update(entity);
            await context.SaveChangesAsync();
            return MapToCore(entity);
        }

        public async Task<Core.Models.Post> Delete(Core.Models.Post post)
        {
            var entity = MapToData(post);
            context.Remove(entity);
            await context.SaveChangesAsync();
            return MapToCore(entity);
        }

        public async Task Delete(int postId)
        {
            var post = context.Posts.AsNoTracking().FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return;
            }
            else
            {
                context.Posts.Remove(post);
                await context.SaveChangesAsync();
            }
        }

        private async Task<bool> Exists(int id)
        {
            var user = await context.Posts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Core.Models.Post MapToCore(Post entity)
        {
            return mapper.Map<Core.Models.Post>(entity);
        }

        protected IEnumerable<Core.Models.Post> MapToCore(IEnumerable<Post> entities)
        {
            return mapper.Map<IEnumerable<Core.Models.Post>>(entities);
        }

        private Post MapToData(Core.Models.Post coreModel)
        {
            return mapper.Map<Post>(coreModel);
        }
    }
}
