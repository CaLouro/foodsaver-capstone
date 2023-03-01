using System.ComponentModel.DataAnnotations;

namespace FoodSaverWebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is requried")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{6,128}$", ErrorMessage = "Invalid password")]
        public string Password { get; set; }

        public LoginModel() { }
        public LoginModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
