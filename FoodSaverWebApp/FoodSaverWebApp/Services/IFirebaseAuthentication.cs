using FoodSaverWebApp.Models;

namespace FoodSaverWebApp.Services
{
    public interface IFirebaseAuthentication
    {
        public Task<string> Registration(AuthModel authModel);
        public Task<string> Login(AuthModel authModel);
        public string Logout(AuthModel authModel);
        public string AdjustErrorMessage(string message);
    }
}
