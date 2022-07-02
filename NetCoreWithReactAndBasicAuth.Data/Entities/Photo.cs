using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetCoreWithReactAndBasicAuth.Data.Entities
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}