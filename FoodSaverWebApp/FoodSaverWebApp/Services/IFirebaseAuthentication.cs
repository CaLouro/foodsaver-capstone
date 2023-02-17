using Firebase.Auth;
using FoodSaverWebApp.Models;

namespace FoodSaverWebApp.Services
{
    public interface IFirebaseAuthentication
    {
        public Task<string> Login(LoginModel loginModel);
        public Task<string> Registration(RegisterModel authModel);
        public FirebaseError LocalizeAuthExceptionMessage(FirebaseError? exception);
        public FirebaseError ExtractFirebaseException(FirebaseAuthException exception);
    }
}
