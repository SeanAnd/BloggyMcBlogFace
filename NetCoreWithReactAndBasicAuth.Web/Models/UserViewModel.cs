using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetCoreWithReactAndBasicAuth.Web.Models
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}