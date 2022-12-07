using System.ComponentModel.DataAnnotations;

namespace RunGroubweb.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name="Email address")]
        [Required(ErrorMessage ="Email is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirm your password")]
        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Passwords are not matched")]
        public string ConfirmPassword { get; set; }

    }
}
