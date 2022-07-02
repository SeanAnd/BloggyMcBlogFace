using Microsoft.EntityFrameworkCore;
using NetCoreWithReactAndBasicAuth.Data.Entities;

namespace NetCoreWithReactAndBasicAuth.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<Photo> Photos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
