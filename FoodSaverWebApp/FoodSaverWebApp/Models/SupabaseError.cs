namespace FoodSaverWebApp.Models
{
    public class LoginError
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }

    public class RegisterError
    {
        public int code { get; set; }
        public string msg { get; set; }
    }
}
