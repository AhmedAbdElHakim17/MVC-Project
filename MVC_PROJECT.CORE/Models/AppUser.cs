using Microsoft.AspNetCore.Identity;
namespace MVC_PROJECT.Models
{
    public class AppUser:IdentityUser
    {
        public string? Address { get; set; }
        public Student? Student { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
