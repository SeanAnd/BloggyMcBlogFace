using Microsoft.AspNetCore.Mvc;
using NetCoreWithReactAndBasicAuth.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWithReactAndBasicAuth.Web.Models
{
    public class PostViewModel
    {
        [Key]
        [FromForm(Name = "id")]
        public int? Id { get; set; }
        [FromForm(Name = "title")]
        public string Title { get; set; }
        [FromForm(Name = "content")]
        public string Content { get; set; }
        [FromForm(Name = "userId")]
        public int? UserId { get; set; }
        public User? User { get; set; }
        [FromForm(Name = "photos")]
        public List<Photo> Photos { get; set; }
    }
}