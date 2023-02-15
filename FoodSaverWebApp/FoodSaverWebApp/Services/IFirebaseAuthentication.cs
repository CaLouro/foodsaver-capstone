using FoodSaverWebApp.Models;

namespace FoodSaverWebApp.Services
{
    public interface IFirebaseAuthentication
    {
        public Task<string> Registration(RegisterModel authModel);
        public Task<string> Login(LoginModel loginModel);
        public string AdjustErrorMessage(string message);
    }
}
