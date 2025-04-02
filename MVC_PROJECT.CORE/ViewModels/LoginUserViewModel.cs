namespace MVC_PROJECT.ViewModels
{
    public class LoginUserViewModel
    {

        [Required(ErrorMessage = "Email is Reqired")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
