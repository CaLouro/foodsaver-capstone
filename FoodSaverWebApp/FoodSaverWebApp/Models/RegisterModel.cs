using System.ComponentModel.DataAnnotations;

namespace FoodSaverWebApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is requried")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{6,128}$", ErrorMessage = "Invalid password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
