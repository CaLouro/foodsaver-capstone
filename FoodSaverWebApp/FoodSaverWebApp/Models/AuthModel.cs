using System.ComponentModel.DataAnnotations;

namespace FoodSaverWebApp.Models
{
    public class AuthModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{6,128}$", ErrorMessage = "Invalid password")]
        public string Password { get; set; }
    }
}
