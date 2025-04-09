using Microsoft.AspNetCore.Identity;
namespace MVC_PROJECT.ViewModels
{
    public class AdminViewModel
    {
        public List<AppUser>? Users { get; set; }
        public List<IdentityRole>? Roles { get; set; }

    }
}
