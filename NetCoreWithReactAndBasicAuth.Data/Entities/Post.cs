using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetCoreWithReactAndBasicAuth.Data.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Photo> Photos { get; set; }
    }
}