using System.ComponentModel.DataAnnotations;
using TripTrotters.Models;

namespace TripTrotters.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        [Required]
        public string Username { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required!")]
        [Compare("Password", ErrorMessage = "Password do not match!")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "User Role")]
        [Required]
        public UserType UserRole { get; set; }
        public IFormFile Image { get; set; }
    }
}
