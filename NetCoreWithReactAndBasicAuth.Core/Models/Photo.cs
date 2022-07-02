using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetCoreWithReactAndBasicAuth.Core.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
        public IFormFile? File { get; set; }
    }
}