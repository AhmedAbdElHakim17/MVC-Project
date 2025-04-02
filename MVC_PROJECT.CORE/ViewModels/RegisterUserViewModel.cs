using Microsoft.AspNetCore.Identity;
namespace MVC_PROJECT.ViewModels
{
    public class RegisterUserViewModel
    {
        public string? UserName { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        //public string Role { get; set; }

        //public List<IdentityRole>? Roles { get; set; }
    }
}
